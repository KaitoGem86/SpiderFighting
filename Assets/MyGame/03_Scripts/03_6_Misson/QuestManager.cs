using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Enemy;
using Core.GamePlay.Mission.NPC;
using Core.GamePlay.MyPlayer;
using CSVLoad;
using MyTools.Event;
using UnityEditor;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance;

        public List<Quest> quests;
        public PlayerData playerData;
        private Quest currentQuest;
        private int currentQuestIndex;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            instance = this;
            StartQuest(0);
        }

        public void StartQuest(int questID)
        {
            currentQuestIndex = questID;
            currentQuest = quests[questID];
            quests[questID].Init();
            currentQuest.StartQuest();
        }

        public void RetryQuest()
        {
            quests[currentQuestIndex].ResetQuest();
            playerData.ResetPlayerStat();
            quests[currentQuestIndex].StartQuest();
        }

        public void Update()
        {
            if (currentQuest != null)
            {
                currentQuest.Update();
            }
        }

        public void NextQuest()
        {
            currentQuestIndex = (currentQuestIndex + 1) % quests.Count;
            currentQuest = quests[currentQuestIndex];
            currentQuest.Init();
            currentQuest.StartQuest();
        }

        public void FinishQuest()
        {
            playerData.ResetPlayerStat();
            playerData.playerSerializeData.rewards[Data.Reward.RewardType.Cash] += currentQuest.reward.currency;
            playerData.UpdateExp(currentQuest.reward.exp);
            playerData.onUpdatePlayerData.Raise();
        }

        #region  LOAD QUEST FROM SCENE
#if UNITY_EDITOR
        enum QuestType
        {
            Fighting,
            Shipping,
            StopCar,
            Protected,
            FightingBoss
        }

        enum QuestStepType
        {
            Receive,
            Send,
            Protect,
            Shipping,
            Fight,
            FightBoss
        }

        public string spreadsheetId;
        public string gid;

        // public void LoadCSV()
        // {
        //     var tmp = MissionConfig.GetMissionConFig(spreadsheetId, gid);
        //     foreach(var mission in tmp){
        //         switch(mission.mission){
        //             case MissionType.Fighting:
        //             case MissionType.FightingBoss:
        //             case MissionType.Protected:
        //                 break;
        //             case MissionType.Shipping:
        //                 break;
        //             case MissionType.StopCar:
        //                 break;
        //         }
        //     }
        // }

        [Header("Quest data storage")]
        [SerializeField] private string questPath;
        [SerializeField] private SerializedDictionary<QuestStepType, GameObject> questStepPrefabs;

        [Header("Enemy data")]
        [SerializeField] private EnemySO unarmEnemy;
        [SerializeField] private EnemySO clubEnemy;
        [SerializeField] private EnemySO pistolEnemy;
        [SerializeField] private EnemySO riffleEnemy;
        [SerializeField] private BossSO bossEnemy;
        
        [Header("NPC data")]
        [SerializeField] private NPCSO npcSO;

        [Header("Quests in scene")]
        [SerializeField] private Transform questParent;

        [Header("Quest events")]
        [SerializeField] private DefaultEvent _onShippingQuestStart;
        [SerializeField] private DefaultEvent _onShippingQuestFinish;

        [ContextMenu("Load Quests")]
        public void LoadQuests()
        {
            quests = new List<Quest>();
            var tmp = GetDictLocation();
            var questData = MissionConfig.GetMissionConFig(spreadsheetId, gid);
            for (int i = 0; i < Mathf.Max(tmp.Count, questData.Length - 1); i++)
            {
                GenerateQuestData(tmp[i], questData[i + 1]);
            }
        }

        private List<KeyValuePair<QuestType, Transform>> GetDictLocation()
        {
            List<KeyValuePair<QuestType, Transform>> dict = new List<KeyValuePair<QuestType, Transform>>();
            foreach (Transform child in questParent)
            {
                if (child.name.Contains("Boss"))
                {
                    dict.Add(new KeyValuePair<QuestType, Transform>(QuestType.FightingBoss, child));
                }
                else if (child.name.Contains("Fighting"))
                {
                    dict.Add(new KeyValuePair<QuestType, Transform>(QuestType.Fighting, child));
                }
                else if (child.name.Contains("Shipping"))
                {
                    dict.Add(new KeyValuePair<QuestType, Transform>(QuestType.Shipping, child));
                }
                else if (child.name.Contains("Stop Car"))
                {
                    dict.Add(new KeyValuePair<QuestType, Transform>(QuestType.StopCar, child));
                }
                else if (child.name.Contains("Protect"))
                {
                    dict.Add(new KeyValuePair<QuestType, Transform>(QuestType.Protected, child));
                }
            }
            return dict;
        }

        private void GenerateQuestData(KeyValuePair<QuestType, Transform> questObject, MissionConfig data)
        {
            var folderPath = questPath + "/" + questObject.Value.name;
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder(questPath, questObject.Value.name);
            }
            var receiveStepData = ScriptableObject.CreateInstance<ShippingQuestInitData>();
            receiveStepData.position = questObject.Value.position;
            AssetDatabase.CreateAsset(receiveStepData, folderPath + $"/Receive_{questObject.Value.name}.asset");
            if (CheckValidTypeQuest(questObject.Key, data.mission))
            {
                switch (data.mission)
                {
                    case MissionType.Fighting:
                    case MissionType.FightingBoss:
                        var questData = ScriptableObject.CreateInstance<Quest>();
                        questData.infor = new QuestInfor
                        {
                            QuestName = questObject.Value.name,
                            QuestDescription = "This is a quest"
                        };
                        questData.reward = new RewardInfor
                        {
                            exp = 100,
                            currency = 100
                        };
                        questData.stepPrefabs = new List<GameObject>();
                        questData.dataPrefabs = new List<ScriptableObject>();
                        questData.stepPrefabs.Add(questStepPrefabs[QuestStepType.Receive]);
                        questData.dataPrefabs.Add(receiveStepData);
                        Dictionary<TierEnemy, int>[] amountEnemy = new Dictionary<TierEnemy, int>[data.enemyWaves];
                        amountEnemy[data.enemyWaves - 1] = new(data.amountEnemy);
                        for (int i = 0; i < data.enemyWaves - 1; i++)
                        {
                            amountEnemy[i] = new Dictionary<TierEnemy, int>();
                            foreach (var enemy in data.amountEnemy)
                            {
                                amountEnemy[i].Add(enemy.Key, enemy.Value / data.enemyWaves);
                                amountEnemy[data.enemyWaves - 1][enemy.Key] -= enemy.Value / data.enemyWaves;
                            }
                        }

                        for (int i = 0; i < data.enemyWaves; i++)
                        {
                            var fightingData = ScriptableObject.CreateInstance<FightingQuestInitData>();
                            fightingData.position = receiveStepData.position;
                            fightingData.data = new List<EnemyData>();
                            foreach (var enemy in amountEnemy[i])
                            {
                                switch (enemy.Key)
                                {
                                    case TierEnemy.Tier1:
                                        fightingData.data.Add(new EnemyData { enemySO = unarmEnemy, count = enemy.Value });
                                        break;
                                    case TierEnemy.Tier2:
                                        fightingData.data.Add(new EnemyData { enemySO = clubEnemy, count = enemy.Value });
                                        break;
                                    case TierEnemy.Elite:
                                        fightingData.data.Add(new EnemyData { enemySO = pistolEnemy, count = enemy.Value });
                                        break;
                                }
                            }
                            if (data.mission == MissionType.FightingBoss)
                            {
                                fightingData.data.Add(new EnemyData { enemySO = bossEnemy, count = 1 });
                            }
                            AssetDatabase.CreateAsset(fightingData, folderPath + $"/Fighting_{i}.asset");
                            questData.stepPrefabs.Add(questStepPrefabs[QuestStepType.Fight]);
                            questData.dataPrefabs.Add(fightingData);
                        }
                        AssetDatabase.CreateAsset(questData, folderPath + $"/{questObject.Value.name}.asset");
                        quests.Add(questData);
                        break;
                    case MissionType.Protected:
                        var questDataProtect = ScriptableObject.CreateInstance<ProtectedQuest>();
                        questDataProtect.infor = new QuestInfor
                        {
                            QuestName = questObject.Value.name,
                            QuestDescription = "This is a quest"
                        };
                        questDataProtect.reward = new RewardInfor
                        {
                            exp = 100,
                            currency = 100
                        };
                        questDataProtect.stepPrefabs = new List<GameObject>();
                        questDataProtect.dataPrefabs = new List<ScriptableObject>();
                        questDataProtect.stepPrefabs.Add(questStepPrefabs[QuestStepType.Receive]);
                        questDataProtect.dataPrefabs.Add(receiveStepData);
                        Dictionary<TierEnemy, int>[] amountEnemyProtected = new Dictionary<TierEnemy, int>[data.enemyWaves];
                        amountEnemyProtected[data.enemyWaves - 1] = new(data.amountEnemy);
                        for (int i = 0; i < data.enemyWaves - 1; i++)
                        {
                            amountEnemyProtected[i] = new Dictionary<TierEnemy, int>();
                            foreach (var enemy in data.amountEnemy)
                            {
                                amountEnemyProtected[i].Add(enemy.Key, enemy.Value / data.enemyWaves);
                                amountEnemyProtected[data.enemyWaves - 1][enemy.Key] -= enemy.Value / data.enemyWaves;
                            }
                        }

                        for (int i = 0; i < data.enemyWaves; i++)
                        {
                            var fightingData = ScriptableObject.CreateInstance<FightingQuestInitData>();
                            fightingData.position = receiveStepData.position;
                            fightingData.data = new List<EnemyData>();
                            foreach (var enemy in amountEnemyProtected[i])
                            {
                                switch (enemy.Key)
                                {
                                    case TierEnemy.Tier1:
                                        fightingData.data.Add(new EnemyData { enemySO = unarmEnemy, count = enemy.Value });
                                        break;
                                    case TierEnemy.Tier2:
                                        fightingData.data.Add(new EnemyData { enemySO = clubEnemy, count = enemy.Value });
                                        break;
                                    case TierEnemy.Elite:
                                        fightingData.data.Add(new EnemyData { enemySO = pistolEnemy, count = enemy.Value });
                                        break;
                                }
                            }
                            if (data.mission == MissionType.FightingBoss)
                            {
                                fightingData.data.Add(new EnemyData { enemySO = bossEnemy, count = 1 });
                            }
                            AssetDatabase.CreateAsset(fightingData, folderPath + $"/Fighting_{i}.asset");
                            questDataProtect.stepPrefabs.Add(questStepPrefabs[QuestStepType.Fight]);
                            questDataProtect.dataPrefabs.Add(fightingData);
                        }
                        questDataProtect.npcSO = npcSO;
                        var random = UnityEngine.Random.insideUnitSphere * 5;
                        random.y = 0;
                        questDataProtect.spawnPosition = receiveStepData.position + random;
                        AssetDatabase.CreateAsset(questDataProtect, folderPath + $"/{questObject.Value.name}.asset");
                        quests.Add(questDataProtect);
                        break;
                    case MissionType.Shipping:
                        var questDataShipping = ScriptableObject.CreateInstance<ShippingQuest>();
                        questDataShipping.time = data.time;
                        questDataShipping.infor = new QuestInfor
                        {
                            QuestName = questObject.Value.name,
                            QuestDescription = "This is a quest"
                        };
                        questDataShipping.reward = new RewardInfor
                        {
                            exp = 100,
                            currency = 100
                        };
                        questDataShipping.stepPrefabs = new List<GameObject>();
                        questDataShipping.dataPrefabs = new List<ScriptableObject>();
                        questDataShipping.stepPrefabs.Add(questStepPrefabs[QuestStepType.Receive]);
                        questDataShipping.dataPrefabs.Add(receiveStepData);

                        foreach (Transform child in questObject.Value)
                        {
                            var shippingStepData = ScriptableObject.CreateInstance<ShippingQuestInitData>();
                            shippingStepData.position = child.position;
                            AssetDatabase.CreateAsset(shippingStepData, folderPath + $"/Shipping_{child.name}.asset");
                            questDataShipping.stepPrefabs.Add(questStepPrefabs[QuestStepType.Shipping]);
                            questDataShipping.dataPrefabs.Add(shippingStepData);
                        }
                        questDataShipping.onFinishShippingQuest = _onShippingQuestFinish;
                        questDataShipping.onStartShippingQuest = _onShippingQuestStart;
                        AssetDatabase.CreateAsset(questDataShipping, folderPath + $"/{questObject.Value.name}.asset");
                        quests.Add(questDataShipping);
                        break;
                    case MissionType.StopCar:
                        break;
                    default:
                        break;
                }


                // var questData = ScriptableObject.CreateInstance<Quest>();
                // questData.infor = new QuestInfor
                // {
                //     QuestName = questObject.Value.name,
                //     QuestDescription = "This is a quest"
                // };
                // questData.reward = new RewardInfor
                // {
                //     exp = 100,
                //     currency = 100
                // };
                // questData.stepPrefabs = new List<GameObject>();
                // questData.dataPrefabs = new List<ScriptableObject>();
                // questData.stepPrefabs.Add(questStepPrefabs[QuestStepType.Receive]);
                // questData.dataPrefabs.Add(receiveStepData);
                // switch (questObject.Key)
                // {
                //     case QuestType.Fighting:
                //         break;
                //     case QuestType.Shipping:
                //         foreach (Transform child in questObject.Value)
                //         {
                //             var shippingStepData = ScriptableObject.CreateInstance<ShippingQuestInitData>();
                //             shippingStepData.position = child.position;
                //             AssetDatabase.CreateAsset(shippingStepData, folderPath + $"/Shipping_{child.name}.asset");
                //             questData.stepPrefabs.Add(questStepPrefabs[QuestStepType.Shipping]);
                //             questData.dataPrefabs.Add(shippingStepData);
                //         }
                //         break;
                //     default:
                //         break;
                // }
                // quests.Add(questData);
                // AssetDatabase.CreateAsset(questData, folderPath + $"/{questObject.Value.name}.asset");
            }
        }


        private bool CheckValidTypeQuest(QuestType type1, MissionType type2)
        {
            if (type1 == QuestType.Fighting && type2 == MissionType.Fighting)
            {
                return true;
            }
            if (type1 == QuestType.FightingBoss && type2 == MissionType.FightingBoss)
            {
                return true;
            }
            if (type1 == QuestType.Protected && type2 == MissionType.Protected)
            {
                return true;
            }
            if (type1 == QuestType.Shipping && type2 == MissionType.Shipping)
            {
                return true;
            }
            if (type1 == QuestType.StopCar && type2 == MissionType.StopCar)
            {
                return true;
            }
            return false;
        }
#endif
        #endregion
    }
}