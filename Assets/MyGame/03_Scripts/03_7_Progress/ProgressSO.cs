using UnityEngine;
using Core.SystemGame.Factory;
using System.Collections.Generic;

namespace Progress{
    [CreateAssetMenu(menuName = "MyGame/ProgressSO")]
    public class ProgressSO : BaseSOWithPool
    {
        [SerializeField] private List<ProgressData> _progressDatas;
        [SerializeField] private List<AchivementProgress> _achivementProgresses;
        public int levelStart;

        public GameObject Spawn(ProgressData data, int currentLevel, int maxReceivedProgress, int levelStart, int index)
        {
            var progress = SpawnObject();
            progress.GetComponent<ProgressElement>().Init(this, data, levelStart + index, GetProgressState(maxReceivedProgress, levelStart, index));
            return progress;
        }

        public List<ProgressData> ProgressDatas => _progressDatas;
        public List<AchivementProgress> AchivementProgresses => _achivementProgresses;

        private ProgressState GetProgressState(int maxReceivedProgress, int levelStart, int index)
        {
            if (maxReceivedProgress >= levelStart + index)
            {
                return ProgressState.Received;
            }
            if (maxReceivedProgress == levelStart + index - 1)
            {
                return ProgressState.CanReceive;
            }
            return ProgressState.Locked;
        }
    }
}