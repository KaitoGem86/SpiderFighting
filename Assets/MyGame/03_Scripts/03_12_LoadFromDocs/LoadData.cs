using Core.GamePlay.Mission;
using UnityEngine;

namespace CSVLoad
{
    public class LoadData : MonoBehaviour
    {
        public string spreadsheetId;
        public string gid;

        public void LoadCSV()
        {
            var tmp = MissionConfig.GetMissionConFig(spreadsheetId, gid);
            foreach(var mission in tmp){
                switch(mission.mission){
                    case MissionType.Fighting:
                    case MissionType.FightingBoss:
                    case MissionType.Protected:
                        break;
                    case MissionType.Shipping:
                        break;
                    case MissionType.StopCar:
                        break;
                }
            }
        }
    }
}