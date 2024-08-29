using System;
using System.Collections.Generic;
using Collectible;
using Core.Manager;
using Core.UI.Popup;
using Data.Stat.Player;
using MyTools.Event;
using Newtonsoft.Json;
using ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    [CreateAssetMenu(menuName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public PlayerStatSO playerStatSO;
        public PlayerDataConfig playerDataConfig;
        public PlayerSerializeData playerSerializeData;
        public Dictionary<PlayerStat, float> localStats;
        public CollectibleSerializeEventListener onCollectReward;
        public DefaultEvent onUpdatePlayerData;
        public bool isInMission = false;


        public void Init()
        {
            if (PlayerPrefs.HasKey("PlayerData"))
            {
                Debug.Log("Init Data 1");
                LoadData();
                playerSerializeData.UpdateDataWhenStartGame();
            }
            else
            {
                Debug.Log("Init Data 2");
                playerSerializeData.InitData(this);
                SaveData();
            }
            localStats = playerStatSO.GetInstancesStats();
            isInMission = false;
            onCollectReward.RegisterListener();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            SaveData();
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        public void SaveData()
        {
            var json = JsonConvert.SerializeObject(playerSerializeData);
            Debug.Log(json);
            PlayerPrefs.SetString("PlayerData", json);
        }

        private void LoadData()
        {
            var json = PlayerPrefs.GetString("PlayerData");
            Debug.Log(json);
            playerSerializeData = JsonConvert.DeserializeObject<PlayerSerializeData>(json);
        }

        public void UpdateStat(PlayerStat key, float value)
        {
            localStats[key] = value;
            SaveData();
        }

        public void CollectReward(CollectibleData data)
        {
            int exp = GetExpToNextLevel() * playerDataConfig.levelCoefficients[playerSerializeData.Level] / 100;
            int cash = exp + 50;
            playerSerializeData.Exp += exp;
            playerSerializeData.rewards[Data.Reward.RewardType.Cash] += cash;
            onUpdatePlayerData.Raise();
            CollectCollectiblePopup.Instance.ShowPopup(new RuntimeRewardData{
                cash = cash,
                exp = exp
            });
        }

        public void ResetPlayerStat()
        {
            localStats = playerStatSO.GetInstancesStats();
            onUpdatePlayerData.Raise();
        }

        public void UpdateExp(int value)
        {
            playerSerializeData.Exp += value;
            int expToNextLevel = GetExpToNextLevel();
            if (playerSerializeData.Exp >= expToNextLevel)
            {
                playerSerializeData.Exp -= expToNextLevel;
                playerSerializeData.Level++;
            }
        }

        public int GetExpToNextLevel()
        {
            int tmp = playerSerializeData.Level + playerDataConfig.levelCoefficients[playerSerializeData.Level];
            return tmp * tmp;
        }

        public double GetTimeFromLastUseGadget(PlayerStat key)
        {
            var time = DateTime.Now - playerSerializeData.lastUseGadgetTime[key];
            return time.TotalSeconds;
        }
    }
}