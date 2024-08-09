using UnityEngine;
using Core.SystemGame.Factory;
using System.Collections.Generic;

namespace Progress
{
    [CreateAssetMenu(menuName = "MyGame/ProgressSO")]
    public class ProgressSO : BaseSOWithPool
    {
        [SerializeField] private List<ProgressData> _progressDatas;
        [SerializeField] private List<AchivementProgress> _achivementProgresses;
        public int levelStart;
        public Sprite icon;

        public GameObject Spawn(ProgressData data, int currentLevel, int maxReceivedProgress, int levelStart, int index)
        {
            var progress = SpawnObject();
            progress.GetComponent<ProgressElement>().Init(this, data, levelStart + index, GetProgressState(currentLevel, maxReceivedProgress, levelStart, index));
            return progress;
        }

        public List<ProgressData> ProgressDatas => _progressDatas;
        public List<AchivementProgress> AchivementProgresses => _achivementProgresses;

        private ProgressState GetProgressState(int currentLevel, int maxReceivedProgress, int levelStart, int index)
        {
            if (maxReceivedProgress > currentLevel) throw new System.Exception("Max received progress must be less than or equal to current level");
            if (levelStart + index == maxReceivedProgress + 1)
                if (currentLevel > maxReceivedProgress) return ProgressState.CanReceive;
            if (levelStart + index < maxReceivedProgress + 1) return ProgressState.Received;
            if (levelStart + index > maxReceivedProgress) return ProgressState.Locked;
            return ProgressState.Locked;
        }
    }
}