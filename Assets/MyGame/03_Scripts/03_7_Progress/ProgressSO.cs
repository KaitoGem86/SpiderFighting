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

        public GameObject Spawn(ProgressData data, int levelToReceive, int levelStart, int index)
        {
            var progress = SpawnObject();
            progress.GetComponent<ProgressElement>().Init(this, data, 1, GetProgressState(levelToReceive, levelStart, index));
            return progress;
        }

        public List<ProgressData> ProgressDatas => _progressDatas;
        public List<AchivementProgress> AchivementProgresses => _achivementProgresses;

        private ProgressState GetProgressState(int levelToReceive, int levelStart, int index)
        {
            if (levelToReceive == levelStart + index)
            {
                return ProgressState.CanReceive;
            }
            if (levelToReceive > levelStart + index)
            {
                return ProgressState.Received;
            }
            return ProgressState.Locked;
        }
    }
}