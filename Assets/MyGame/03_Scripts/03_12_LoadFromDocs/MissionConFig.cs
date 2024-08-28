using System;
using System.Collections.Generic;

namespace CSVLoad
{
    public class MissionConfig
    {
        public int stt;
        public MissionType mission;
        public int level;
        public TypeEnemy[] typeEnemy;
        public Dictionary<TierEnemy, int> amountEnemy;
        public int totalEnemy;
        public int enemyWaves;
        public int time;

        public static MissionConfig[] GetMissionConFig(string spreadsheetId, string gid)
        {
            string[] data = CSVLoader.LoadCSV(spreadsheetId, gid);
            if (data == null || data.Length == 0) return null;
            MissionConfig[] result = new MissionConfig[data.Length - 2];
            int currentLevel = 0;
            int enemyWaves = 0;
            for (int i = 2; i < data.Length - 1; i++)
            {
                string[] row = data[i].Split(',');
                var partData = new MissionConfig
                {
                    stt = int.Parse(row[0]),
                    mission = GetMissionType(row[1]),
                    level = int.TryParse(row[2], out int c) ? c : currentLevel,
                    enemyWaves = int.TryParse(row[9], out int e) ? e : enemyWaves
                };
                currentLevel = partData.level;
                enemyWaves = partData.enemyWaves;
                switch (partData.mission)
                {
                    case MissionType.Fighting:
                    case MissionType.FightingBoss:
                    case MissionType.Protected:
                        partData.typeEnemy = GetTypeEnemies(row[3]);
                        partData.amountEnemy = new Dictionary<TierEnemy, int>();
                        if (int.TryParse(row[4], out int tier1)) { partData.amountEnemy.Add(TierEnemy.Tier1, tier1); }
                        if (int.TryParse(row[5], out int tier2)) { partData.amountEnemy.Add(TierEnemy.Tier2, tier2); }
                        if (int.TryParse(row[6], out int elite)) { partData.amountEnemy.Add(TierEnemy.Elite, elite); }
                        if (int.TryParse(row[7], out int total)) { partData.totalEnemy = total; }
                        else { throw new Exception("Can't parse total"); }
                        break;
                    case MissionType.Shipping:
                        if (int.TryParse(row[8], out int amount)) { partData.time = amount; }
                        else { throw new Exception("Can't parse amount"); }
                        break;
                    default:
                        break;
                }
                result[i - 1] = partData;
            }
            return result;
        }

        private static MissionType GetMissionType(string type)
        {
            if (type.Contains("Boss")) return MissionType.FightingBoss;
            if (type.Contains("Protect")) return MissionType.Protected;
            if (type.Contains("Stop")) return MissionType.StopCar;
            if (type.Contains("Shipping")) return MissionType.Shipping;
            return MissionType.Fighting;
        }

        private static TypeEnemy[] GetTypeEnemies(string type)
        {
            List<TypeEnemy> result = new List<TypeEnemy>();
            if (type.Contains("Gangster")) result.Add(TypeEnemy.Gangster);
            if (type.Contains("Prisoner")) result.Add(TypeEnemy.Prisoner);
            if (type.Contains("Kingpin")) result.Add(TypeEnemy.Kingpin);
            if (type.Contains("Venom")) result.Add(TypeEnemy.Kingpin);
            return result.ToArray();
        }
    }

    public class MissionRewardData{
        public int cash;
        public int exp;

        public static MissionRewardData[] GetMissionRewardDatas(string spreadsheetId, string gid)
        {
            string[] data = CSVLoader.LoadCSV(spreadsheetId, gid);
            if (data == null || data.Length == 0) return null;
            MissionRewardData[] result = new MissionRewardData[data.Length - 2];
            for (int i = 2; i < data.Length - 1; i++)
            {
                string[] row = data[i].Split(',');
                var partData = new MissionRewardData
                {
                    cash = int.TryParse(row[5], out int c) ? c : 0,
                    exp = int.TryParse(row[6], out int e) ? e : 0
                };
                result[i - 1] = partData;
            }
            return result;
        }
    }
}