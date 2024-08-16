using System.Collections.Generic;
using Progress;
using UnityEngine;

namespace DailyReward{
    [CreateAssetMenu(fileName = "New DailyReward", menuName = "DailyReward/New DailyReward")]
    public class DailyRewardSO : ScriptableObject
    {
        public List<ProgressData> datas;
    }
}