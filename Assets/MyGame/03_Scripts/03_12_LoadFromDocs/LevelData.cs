using System.Collections.Generic;
using UnityEngine;

namespace CSVLoad{
    public class LevelData {
        public int levelPlayer;
        public int coefficient;

        public static LevelData[] LoadLevelData(string spreadsheetId, string gid, int levelToLoad){
            string[] data = CSVLoader.LoadCSV(spreadsheetId, gid);
            if (data == null || data.Length == 0) return null;
            LevelData[] result = new LevelData[Mathf.Min(levelToLoad, data.Length - 1)];
            for (int i = 2; i < Mathf.Min(levelToLoad, data.Length - 1); i++){
                string[] row = data[i].Split(',');
                var partData = new LevelData{
                    levelPlayer = int.Parse(row[0]),
                    coefficient = int.Parse(row[7])
                };
                result[i - 1] = partData;
            }
            return result;
        }
    }
}