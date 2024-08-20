using System.Collections.Generic;
using Data.Reward;

namespace CSVLoad{
    public class MissionData {
        public int level;
        public int missionIndex;
        public MissionType missionType;
        public float missionCoefficient;
        public Dictionary<RewardType, int> missionReward;
        
    }
}