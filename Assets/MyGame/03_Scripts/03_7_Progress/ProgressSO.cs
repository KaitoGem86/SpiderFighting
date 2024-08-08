using UnityEngine;
using Core.SystemGame.Factory;
using System.Collections.Generic;

namespace Progress{
    [CreateAssetMenu(menuName = "MyGame/ProgressSO")]
    public class ProgressSO : BaseSOWithPool
    {
        [SerializeField] private List<ProgressData> _progressDatas;
        [SerializeField] private List<AchivementProgress> _achivementProgresses;

        public GameObject Spawn(ProgressData data)
        {
            var progress = SpawnObject();
            progress.GetComponent<ProgressData>();
            return progress;
        }
    }
}