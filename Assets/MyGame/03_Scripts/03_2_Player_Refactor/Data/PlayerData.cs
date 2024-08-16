using System.Collections.Generic;
using Data.Stat.Player;
using Newtonsoft.Json;
using ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    [CreateAssetMenu(menuName = "PlayerData")]
    public class PlayerData : ScriptableObject{
        public PlayerStatSO playerStatSO;
        public PlayerSerializeData playerSerializeData;
        public Dictionary<PlayerStat, float> localStats;

        public void Init(){
            if(PlayerPrefs.HasKey("PlayerData")){
                Debug.Log("Init Data 1");
                LoadData();
                playerSerializeData.UpdateDataWhenStartGame();
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

        private void OnApplicationQuit()
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

        public void UpdateStat(PlayerStat key, float value){
            localStats[key] = value;
            SaveData();
        }
    }
}