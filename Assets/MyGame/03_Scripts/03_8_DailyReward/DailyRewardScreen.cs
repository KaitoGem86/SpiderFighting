using System;
using System.Collections.Generic;
using Core.GamePlay.MyPlayer;
using MyTools.ScreenSystem;
using UnityEngine;

namespace DailyReward
{
    public class DailyRewardScreen : _BaseScreen
    {
        [SerializeField] private DailyRewardSO _dailyRewardSO;
        [SerializeField] private List<RewardElement> _rewardElements;
        [SerializeField] private PlayerData _playerData;
        private int _currentContinuousDay;

        public void CloseScreen()
        {
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.GamePlay);
        }

        protected override void OnCompleteShowItSelf()
        {
            base.OnCompleteShowItSelf();
            var now = DateTime.Now;
            var lastRewardDate = _playerData.playerSerializeData.lastReceiveData;
            _currentContinuousDay = _playerData.playerSerializeData.currentContinuousDay;
            Debug.Log("Last reward date: " + _currentContinuousDay);

            if (!_playerData.playerSerializeData.isClaimedDailyReward){
                
                if (now - lastRewardDate > TimeSpan.FromDays(1))
                {
                    _currentContinuousDay = 0;
                }
            }

            for (int i = 0; i < _rewardElements.Count; i++)
            {
                if (i == _currentContinuousDay)
                {
                    _rewardElements[i].Init(_dailyRewardSO.datas[i], i, _playerData.playerSerializeData.isClaimedDailyReward ? Progress.ProgressState.Locked : Progress.ProgressState.CanReceive);
                }
                else if (i < _currentContinuousDay)
                {
                    _rewardElements[i].Init(_dailyRewardSO.datas[i], i, Progress.ProgressState.Received);
                }
                else
                {
                    _rewardElements[i].Init(_dailyRewardSO.datas[i], i, Progress.ProgressState.Locked);
                }
            }
        }

        public void OnClaimReward(){
            _playerData.playerSerializeData.currentContinuousDay = (_playerData.playerSerializeData.currentContinuousDay + 1) % 7;
            var reward = _dailyRewardSO.datas[_playerData.playerSerializeData.currentContinuousDay];
            _playerData.playerSerializeData.lastReceiveData = DateTime.Now;
            Debug.Log(_playerData.playerSerializeData.currentContinuousDay);
            _playerData.playerSerializeData.isClaimedDailyReward = true;
        }
    }
}