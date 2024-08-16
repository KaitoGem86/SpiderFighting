using System.Collections.Generic;
using MyTools.ScreenSystem;
using UnityEngine;

namespace DailyReward{
    public class DailyRewardScreen : _BaseScreen{
        [SerializeField] private DailyRewardSO _dailyRewardSO;
        [SerializeField] private List<RewardElement> _rewardElements;

        public void CloseScreen(){
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.GamePlay);
        }

        protected override void OnCompleteShowItSelf()
        {
            base.OnCompleteShowItSelf();
            for (int i = 0; i < _rewardElements.Count; i++)
            {
                _rewardElements[i].Init(_dailyRewardSO.datas[i], i, Progress.ProgressState.CanReceive);
            }
        }
    }
}