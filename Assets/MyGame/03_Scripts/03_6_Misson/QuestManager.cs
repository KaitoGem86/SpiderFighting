using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using MyTools.ScreenSystem;
using UnityEditor;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance;

        public List<Quest> quests;
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

        public void NextQuest()
        {
            currentQuestIndex = (currentQuestIndex + 1) % quests.Count;
            currentQuest = quests[currentQuestIndex];
            currentQuest.Init();
            currentQuest.StartQuest();
        }

        public void FinishQuest()
        {
            currentQuest.FinishQuest();
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

        [Header("Quest data storage")]
        [SerializeField] private string questPath;
        [SerializeField] private SerializedDictionary<QuestStepType, GameObject> questStepPrefabs;

        [Header("Quests in scene")]
        [SerializeField] private Transform questParent;

        [ContextMenu("Load Quests")]
        public void LoadQuests()
        {
            quests = new List<Quest>();
            var tmp = GetDictLocation();
            foreach (var quest in tmp)
            {
                GenerateQuestData(quest);
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

        private void GenerateQuestData(KeyValuePair<QuestType, Transform> questObject)
        {
            var folderPath = questPath + "/" + questObject.Value.name;
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder(questPath, questObject.Value.name);
            }
            var receiveStepData = ScriptableObject.CreateInstance<ShippingQuestInitData>();
            receiveStepData.position = questObject.Value.position;
            AssetDatabase.CreateAsset(receiveStepData, folderPath + $"/Receive_{questObject.Value.name}.asset");
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
            switch (questObject.Key)
            {
                case QuestType.Fighting:
                    break;
                case QuestType.Shipping:
                    foreach(Transform child in questObject.Value)
                    {
                        var shippingStepData = ScriptableObject.CreateInstance<ShippingQuestInitData>();
                        shippingStepData.position = child.position;
                        AssetDatabase.CreateAsset(shippingStepData, folderPath + $"/Shipping_{child.name}.asset");
                        questData.stepPrefabs.Add(questStepPrefabs[QuestStepType.Shipping]);
                        questData.dataPrefabs.Add(shippingStepData);
                    }
                    break;
                default:
                    break;
            }
            quests.Add(questData);
            AssetDatabase.CreateAsset(questData, folderPath + $"/{questObject.Value.name}.asset");
        }
#endif
        #endregion
    }
}