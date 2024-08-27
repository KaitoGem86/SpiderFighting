using UnityEngine;
using PopupSystem;
using UnityEngine.UI;
using DG.Tweening;

namespace Core.UI.Popup{
    public class FadeScenePopup : SingletonPopup<FadeScenePopup>
    {
        [SerializeField] private Image _fadeImage;

        public void Show(float delayTime, float fadeTime){
            _fadeImage.gameObject.SetActive(true);
            _fadeImage.DOFade(1f, fadeTime).OnComplete(() => {
                _fadeImage.DOFade(0f, fadeTime).SetDelay(delayTime).OnComplete(() => {
                    _fadeImage.gameObject.SetActive(false);
                    Hide();
                });
            });
        }
    }
}