using MyTools.Event;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace DailyReward{
    public class RewardElement : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _claimButton;
        [SerializeField] private GameObject _receivedIcon;
        [SerializeField] private GameObject _lockedIcon;
        [SerializeField] private IntEvent _onClaimReward;
        private ProgressData _data;
        private int _level;

        public void Init(ProgressData data, int level, ProgressState state)
        {
            _data = data;
            _level = level;
            _icon.sprite = data.icon;
            switch (state)
            {
                case ProgressState.Received:
                    _claimButton.SetActive(false);
                    _receivedIcon.SetActive(true);
                    _lockedIcon.SetActive(false);
                    break;
                case ProgressState.CanReceive:
                    _claimButton.SetActive(true);
                    _receivedIcon.SetActive(false);
                    _lockedIcon.SetActive(false);
                    break;
                case ProgressState.Locked:
                    _claimButton.SetActive(false);
                    _receivedIcon.SetActive(false);
                    _lockedIcon.SetActive(true);
                    break;
            }
        }

        public void ClaimReward()
        {
            Debug.Log("Claim Reward");
            _onClaimReward?.Raise(_level);
            _claimButton.SetActive(false);
            _receivedIcon.SetActive(true);
            _lockedIcon.SetActive(false);
        }
    }
}