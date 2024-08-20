using System.Threading.Tasks;
using UnityEngine.Networking;

namespace CSVLoad{
    public enum MissionType{
        Fighting,
        Shipping,
        FightingBoss,
        Protected,
        StopCar,
    }

    public enum TypeEnemy{
        Gangster,
        Prisoner,
        Kingpin,
        Venom,
    }

    public enum TierEnemy{
        Tier1,
        Tier2,
        Elite,
    }

    public class CSVLoader {
        private const string _header = "https://docs.google.com/spreadsheets/d/";
        private static string _spreadsheetId;
        private static string _gid;
        private static string _url => $"{_header}{_spreadsheetId}/export?format=csv&gid={_gid}";
        
        public static string[] LoadCSV(string spreadsheetId, string gid){
            _spreadsheetId = spreadsheetId;
            _gid = gid;
            var request = UnityWebRequest.Get(_url);
            request.SendWebRequest();
            while (!request.isDone) { }
            return request.downloadHandler.text.Split('\n');
        }
    }
}