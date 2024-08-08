using Core.GamePlay.MyPlayer;
using Data.Reward;
using UnityEngine;

namespace Progress{
    [System.Serializable]
    public class ProgressData {
        public Sprite receivedBackground;
        public Sprite canReceiveBackground;
        public Sprite lockedBackground;
        public Sprite icon;
        public string name;
        public RewardType rewardType;
        public int rewardAmount;
    }

    [System.Serializable]
    public class AchivementProgress{
        public Sprite icon;
        public AchivementType achivementType;
        public int targetAmount;
        public string name;
    }
}