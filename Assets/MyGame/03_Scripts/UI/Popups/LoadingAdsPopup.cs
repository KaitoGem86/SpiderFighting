using UnityEngine;
using PopupSystem;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Core.UI.Popup{
    public class LoadingAdsPopup : SingletonPopup<LoadingAdsPopup> {

        [Header("=================== Animation elements =====================")]
        [SerializeField] private GameObject _loading;
        [SerializeField] private GameObject _loadingComplete;
        [SerializeField] private RectTransform _loadingIcon;

        public override void Awake(){
            base.Awake();
        }

        public void ShowPopup(Action callBack){
            Show(() => AnimationShow(callBack));
        }

        private void AnimationShow(Action callBack){
            _loading.SetActive(true);
            _loadingIcon.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                .SetLoops(5, LoopType.Incremental)
                .OnComplete(() => {
                    _loading.SetActive(false);
                    _loadingComplete.SetActive(true);
                    DOVirtual.DelayedCall(1.5f, () => {
                        _loadingComplete.SetActive(false);
                        Hide(callBack);
                    });
                });
        }
    }
}