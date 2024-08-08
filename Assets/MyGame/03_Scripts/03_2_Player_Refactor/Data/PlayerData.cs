using System.Collections.Generic;
using Data.Stat.Player;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class PlayerData : MonoBehaviour{
        public PlayerStatSO playerStatSO;
        public PlayerSerializeData playerSerializeData;
        public Dictionary<PlayerStat, float> localStats;

        private void Awake(){
            if(PlayerPrefs.HasKey("PlayerData")){
                Debug.Log("Init Data 1");
                LoadData();
            }
            else{
                Debug.Log("Init Data 2");
                playerSerializeData.InitData();
                SaveData();
            }
            localStats = playerStatSO.GetInstancesStats();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            SaveData();
        }

        private void SaveData(){
            var json = JsonConvert.SerializeObject(playerSerializeData);
            Debug.Log(json);
            PlayerPrefs.SetString("PlayerData", json);
        }

        private void LoadData(){
            var json = PlayerPrefs.GetString("PlayerData");
            playerSerializeData = JsonConvert.DeserializeObject<PlayerSerializeData>(json);
        }
    }
}