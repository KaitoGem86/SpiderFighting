using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Extensions.CooldownButton{
        [System.Serializable]
    public class CoolDownButton
    {
        public Selectable button;
        public Image fillImage;
        public TMP_Text text;

        public void SetCoolDown(float time)
        {
            button.interactable = false;
            fillImage.fillAmount = 1;
            fillImage.gameObject.SetActive(true);
            fillImage.DOFillAmount(0, time)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
                {
                    button.interactable = true;
                    fillImage.gameObject.SetActive(false);
                })
            .OnUpdate(() =>
                {
                    text.text = (fillImage.fillAmount * time).ToString("0.0");
                });
        }

        public void SetCoolDown(float startNormalizeTime, float duration)
        {
            if(startNormalizeTime < 0)
            {
                throw new System.Exception("StartNormalizeTime must be more than 1");
            }
            if(startNormalizeTime >= 1)
            {
                StopCoolDown();
                return;
            }
            button.interactable = false;
            fillImage.fillAmount = startNormalizeTime;
            fillImage.gameObject.SetActive(true);
            fillImage.DOFillAmount(0, duration)
                .OnComplete(() =>
                {
                    button.interactable = true;
                    fillImage.gameObject.SetActive(false);
                })
                .OnUpdate(() =>
                {
                    text.text = (fillImage.fillAmount * duration).ToString("0.0");
                });
        }

        public void StopCoolDown()
        {
            button.interactable = true;
            fillImage.DOKill();
            fillImage.fillAmount = 0;
            text.text = "";
            fillImage.gameObject.SetActive(false);
        }
    }

}