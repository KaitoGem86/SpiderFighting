using Newtonsoft;
using Newtonsoft.Json;
using UnityEngine;

namespace Data.SerializeData{
    [CreateAssetMenu(menuName = "MyGame/SerializeData")]
    public class SerializeData : ScriptableObject{
        public void SaveData<T>(T data, string key){
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(key, json);
        }

        public void LoadData<T>(ref T data, string key){
            var json = PlayerPrefs.GetString(key);
            data = JsonUtility.FromJson<T>(json);
        }
    }
}