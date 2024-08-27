using System;
using UnityEngine;
using PopupSystem;
using UnityEngine.UI;
using TMPro;
using Collectible;
using DG.Tweening;

namespace Core.UI.Popup
{
    public class CollectCollectiblePopup : SingletonPopup<CollectCollectiblePopup>
    {
        [Header("=================== Animation elements =====================")]
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private RectTransform _cash;
        [SerializeField] private RectTransform _exp;
        [SerializeField] private RectTransform _titleFrame;
        [SerializeField] private RectTransform _acceptButton;

        [Header("=================== Data elements =====================")]
        [SerializeField] private TMP_Text _cashText;
        [SerializeField] private TMP_Text _expText;

        public void ShowPopup(CollectibleRewardData data)
        {
            _cashText.text = "+" + data.cash.ToString();
            _expText.text = "+" + data.exp.ToString();
            Show(AnimationShow);
        }

        public void HidePopup()
        {
            LoadingAdsPopup.Instance.ShowPopup(AnimationHide);
            //AnimationHide();
        }

        private void AnimationShow()
        {
            _backgroundImage.fillAmount = 0;
            _backgroundImage.gameObject.SetActive(true);
            _backgroundImage.DOFillAmount(1, 0.3f).OnComplete(
                () =>
                {
                    _titleFrame.localScale = new Vector3(1, 0, 1);
                    _titleFrame.gameObject.SetActive(true);
                    _titleFrame.DOScaleY(1, 0.2f).OnComplete(
                        () =>
                        {
                            _cash.localScale = new Vector3(0, 0, 0);
                            _cash.gameObject.SetActive(true);
                            _cash.DOScale(Vector3.one, 0.2f);
                            _exp.localScale = new Vector3(0, 0, 0);
                            _exp.gameObject.SetActive(true);
                            _exp.DOScale(Vector3.one, 0.2f).OnComplete(
                                () =>
                                {
                                    _acceptButton.localScale = new Vector3(0, 0, 0);
                                    _acceptButton.gameObject.SetActive(true);
                                    _acceptButton.DOScale(Vector3.one, 0.3f);
                                }
                            );
                        }
                    );
                }
            );
        }

        private void AnimationHide()
        {
            _acceptButton.DOScale(Vector3.zero, 0.2f).OnComplete(
                () =>
                {
                    _acceptButton.gameObject.SetActive(false);
                    _cash.DOScale(Vector3.zero, 0.5f).OnComplete(
                        () =>{
                            _cash.gameObject.SetActive(false);
                    });
                    _exp.DOScale(Vector3.zero, 0.5f).OnComplete(
                        () =>
                        {
                            _exp.gameObject.SetActive(false);
                            _titleFrame.DOScaleY(0, 0.5f).OnComplete(
                                () =>
                                {
                                    _titleFrame.gameObject.SetActive(false);
                                    _backgroundImage.DOFillAmount(0, 0.5f).OnComplete(
                                        () =>
                                        {
                                            _backgroundImage.gameObject.SetActive(false);
                                            Hide();
                                        }
                                    );
                                }
                            );
                        }
                    );
                }
            );
        }
    }
}