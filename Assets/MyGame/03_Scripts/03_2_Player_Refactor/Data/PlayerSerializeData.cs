using System;
using System.Collections.Generic;
using Data.Reward;

namespace Core.GamePlay.MyPlayer
{
    [System.Serializable]
    public class PlayerSerializeData
    {
        public int Exp;
        public int Level;
        public int skinIndex;
        public int gadgetIndex;
        public int missionIndex;
        public Dictionary<RewardType, int> rewards;
        public Dictionary<AchivementType, int> achivements;
        public int maxReceiveProgress;
        public DateTime lastReceiveData;
        public int currentContinuousDay;
        public bool isClaimedDailyReward;

        public void InitData()
        {
            Exp = 0;
            Level = 1;
            missionIndex = 0;
            skinIndex = 2;
            gadgetIndex = 0;
            rewards = new Dictionary<RewardType, int>();
            foreach (var rewardType in RewardType.GetValues(typeof(RewardType)))
            {
                rewards.Add((RewardType)rewardType, 0);
            }
            achivements = new Dictionary<AchivementType, int>();
            foreach (var achivementType in AchivementType.GetValues(typeof(AchivementType)))
            {
                achivements.Add((AchivementType)achivementType, 0);
            }
            maxReceiveProgress = 0;
            lastReceiveData = DateTime.Now;
            currentContinuousDay = 0;
            isClaimedDailyReward = false;
        }

        public void UpdateDataWhenStartGame(){
            if (DateTime.Now.Date - lastReceiveData.Date == TimeSpan.FromDays(1))
            {
                isClaimedDailyReward = false;
            }
        }
    }
}