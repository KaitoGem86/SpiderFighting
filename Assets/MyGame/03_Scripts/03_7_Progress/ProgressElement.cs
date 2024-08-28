using Core.UI.Popup;
using MyTools.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Progress
{
    public enum ProgressState
    {
        Received,
        CanReceive,
        Locked
    }

    public class ProgressElement : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _claimButton;
        [SerializeField] private GameObject _receivedIcon;
        [SerializeField] private GameObject _lockedIcon;
        [SerializeField] private IntEvent _onClaimReward;
        private ProgressData _data;
        private int _level;

        public void Init(ProgressSO so, ProgressData data, int level, ProgressState state)
        {
            _data = data;
            _levelText.text = level.ToString();
            _level = level;
            _name.text = data.name;
            _icon.sprite = data.icon;
            switch (state)
            {
                case ProgressState.Received:
                    _background.sprite = data.receivedBackground;
                    _claimButton.SetActive(false);
                    _receivedIcon.SetActive(true);
                    _lockedIcon.SetActive(false);
                    break;
                case ProgressState.CanReceive:
                    _background.sprite = data.canReceiveBackground;
                    _claimButton.SetActive(true);
                    _receivedIcon.SetActive(false);
                    _lockedIcon.SetActive(false);
                    break;
                case ProgressState.Locked:
                    _background.sprite = data.lockedBackground;
                    _claimButton.SetActive(false);
                    _receivedIcon.SetActive(false);
                    _lockedIcon.SetActive(true);
                    break;
            }
        }

        public void ClaimReward()
        {
            LoadingAdsPopup.Instance.ShowPopup(
                () =>
                {
                    Debug.Log("Claim Reward");
                    _onClaimReward?.Raise(_level);
                    _background.sprite = _data.receivedBackground;
                    _claimButton.SetActive(false);
                    _receivedIcon.SetActive(true);
                    _lockedIcon.SetActive(false);
                }
            );
        }

    }
}