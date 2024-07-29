using Newtonsoft.Json;
using UnityEngine;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(menuName = "MyGame/StatDatasController")]
    public class StatDatasController : ScriptableObject{
        [SerializeField] private int _id;
        public StatData[] StatDatas;

        public void SaveData(){
            var json = JsonConvert.SerializeObject(StatDatas);
            PlayerPrefs.SetString("StatDatas " + _id, json);
        }

        public void LoadData(){
            var json = PlayerPrefs.GetString("StatDatas " + _id);
            StatDatas = JsonConvert.DeserializeObject<StatData[]>(json);
        }
    }
}