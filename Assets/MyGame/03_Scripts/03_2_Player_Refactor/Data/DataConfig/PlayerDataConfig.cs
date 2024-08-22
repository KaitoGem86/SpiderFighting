using CSVLoad;
using UnityEditor;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    [CreateAssetMenu(menuName = "PlayerData/PlayerDataConfig")]
    public class PlayerDataConfig : ScriptableObject
    {
        public int[] levelCoefficients;
        [SerializeField] private int _levelToLoad;
        [SerializeField] private string _spreadsheetId;
        [SerializeField] private string _gid;


        public void LoadDataFromCSV()
        {
            LevelData[] levelDatas = LevelData.LoadLevelData(_spreadsheetId, _gid, _levelToLoad);
            if (levelDatas == null) return;
            levelCoefficients = new int[levelDatas.Length];
            foreach (var levelData in levelDatas)
            {
                if(levelData == null) continue;
                levelCoefficients[levelData.levelPlayer] = levelData.coefficient;
            }
        }
    }

    [CustomEditor(typeof(PlayerDataConfig))]
    public class PlayerDataConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PlayerDataConfig myScript = (PlayerDataConfig)target;
            if (GUILayout.Button("Load Data From CSV"))
            {
                myScript.LoadDataFromCSV();
            }
        }
    }


}