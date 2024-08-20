using UnityEngine;

namespace CSVLoad
{
    public class LoadData : MonoBehaviour
    {
        public string spreadsheetId;
        public string gid;

        [ContextMenu("Load CSV")]
        public void LoadCSV()
        {
            MissionConfig.GetMissionConFig(spreadsheetId, gid);
        }
    }
}