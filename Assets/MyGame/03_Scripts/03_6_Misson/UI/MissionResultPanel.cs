using Core.UI.Popup;
using DG.Tweening;
using MyTools.Event;
using MyTools.ScreenSystem;
using PopupSystem;
using TMPro;
using UnityEngine;

namespace Core.GamePlay.Mission.Protected
{
    public class MissionResultPanel : SingletonPopup<MissionResultPanel>
    {

        [Header("========= ANIMATION ELEMENTs =========")]
        [Header("Completed Panel")]
        [SerializeField] private RectTransform _panel;
        [SerializeField] private RectTransform _watchAdsButton;
        [SerializeField] private RectTransform _nextButton;

        [Header("Failed Panel")]
        [SerializeField] private RectTransform _failedPanel;
        [SerializeField] private RectTransform _watchAdsButtonFailed;
        [SerializeField] private RectTransform _retryButton;


        [Header("========= CONTROL ELEMENTS =========")]
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private TMP_Text _expText;
        [SerializeField] private TMP_Text _cashText;
        [SerializeField] private GameObject _successButtons;
        [SerializeField] private GameObject _failButtons;
        [SerializeField] private DefaultEvent _onNextMission;
        [SerializeField] private DefaultEvent _onRetryMission;

        private bool _isCompleted = false;

        public void Show(bool isMissionSuccess, RewardInfor rewardInfor)
        {
            _isCompleted = isMissionSuccess;
            Show();
            OnShow(isMissionSuccess, rewardInfor);
            AnimationShow();
        }

        public void OnShow(bool isMissionSuccess, RewardInfor rewardInfor)
        {
            _resultText.text = isMissionSuccess ? "MISSION SUCCESS" : "MISSION FAILED";
            _successButtons.SetActive(isMissionSuccess);
            _failButtons.SetActive(!isMissionSuccess);
            if (!isMissionSuccess) return;
            _expText.text = "+ " + rewardInfor.exp.ToString();
            _cashText.text = "+ " + rewardInfor.currency.ToString();
        }

        public void WatchAdsToNext()
        {
            LoadingAdsPopup.Instance.ShowPopup(
                () =>
                {
                    AnimationHide();
                    _onNextMission.Raise();
                }
            );
        }

        public void OnNextMission()
        {
            AnimationHide();
            _onNextMission.Raise();
        }

        public void OnRetryMission()
        {
            AnimationHide();
            _onRetryMission.Raise();
        }

        private void AnimationShow()
        {
            if (_isCompleted)
            {
                _panel.DOScale(Vector3.one, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        _watchAdsButton.DOScale(Vector3.one, 0.5f)
                            .SetEase(Ease.OutBack)
                            .OnComplete(() =>
                            {
                                _watchAdsButton.DOScale(Vector3.one * 1.2f, 0.5f)
                                    .SetLoops(-1, LoopType.Yoyo)
                                    .SetEase(Ease.OutBack);
                                _nextButton.DOScale(Vector3.one, 0.5f)
                                    .SetDelay(2)
                                    .SetEase(Ease.OutBack);
                            });
                    });
            }
            else
            {
                _failedPanel.DOScale(Vector3.one, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        _watchAdsButtonFailed.DOScale(Vector3.one, 0.5f)
                            .SetEase(Ease.OutBack)
                            .OnComplete(() =>
                            {
                                _watchAdsButtonFailed.DOScale(Vector3.one * 1.2f, 0.5f)
                                    .SetLoops(-1, LoopType.Yoyo)
                                    .SetEase(Ease.OutBack);
                                _retryButton.DOScale(Vector3.one, 0.5f)
                                    .SetDelay(2)
                                    .SetEase(Ease.OutBack);
                            });
                    });
            }
        }

        private void AnimationHide()
        {
            if (_isCompleted)
            {
                _watchAdsButton.DOKill();
                _nextButton.DOKill();
                _panel.DOScale(Vector3.zero, 0.5f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        _watchAdsButton.localScale = Vector3.zero;
                        _nextButton.localScale = Vector3.zero;
                        Hide();
                    });
            }
            else
            {
                _watchAdsButtonFailed.DOKill();
                _retryButton.DOKill();
                _failedPanel.DOScale(Vector3.zero, 0.5f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        _watchAdsButtonFailed.localScale = Vector3.zero;
                        _retryButton.localScale = Vector3.zero;
                        Hide();
                    });
            }
        }
    }
}