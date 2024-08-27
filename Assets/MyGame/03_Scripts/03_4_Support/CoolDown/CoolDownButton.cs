using System;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Extensions.CooldownButton
{
    [System.Serializable]
    public class CoolDownButton
    {
        public Selectable button;
        public Image fillImage;
        public TMP_Text text;
        public TMP_Text stackText;


        public void SetCoolDown(float time, Action completeCallBack = null)
        {
            button.interactable = false;
            fillImage.fillAmount = 1;
            fillImage.gameObject.SetActive(true);
            fillImage.DOKill();
            fillImage.DOFillAmount(0, time)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
                {
                    button.interactable = true;
                    fillImage.gameObject.SetActive(false);
                    completeCallBack?.Invoke();
                })
            .OnUpdate(() =>
                {
                    text.text = (fillImage.fillAmount * time).ToString("0.0");
                });
        }

        public void SetCoolDown(float time, int stack, Action completeCallBack = null)
        {
            if (stack == 0)
            {
                button.interactable = false;
                stackText.text = "";
            }
            else if (stack == 1)
            {
                stackText.text = "1";
            }
            else if (stack > 1)
            {
                stackText.text = stack.ToString();
            }
            fillImage.fillAmount = 1;
            fillImage.gameObject.SetActive(true);
            fillImage.DOKill();
            fillImage.DOFillAmount(0, time)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
                {
                    button.interactable = true;
                    fillImage.gameObject.SetActive(false);
                    stack += 1;
                    if (stack == 0)
                    {
                        button.interactable = false;
                        stackText.text = "";
                    }
                    else if (stack == 1)
                    {
                        stackText.text = "1";
                    }
                    else if (stack > 1)
                    {
                        stackText.text = stack.ToString();
                    }
                    completeCallBack?.Invoke();
                })
            .OnUpdate(() =>
                {
                    text.text = (fillImage.fillAmount * time).ToString("0.0");
                });
        }

        /// <summary>
        /// current stack là số lần sử dụng còn lại của skill sau khi đã sử dụng
        /// </summary>
        /// <param name="time"></param>
        /// <param name="currentStack"></param>
        public void SetCoolDown(float time ,ref int currentStack, Action completeCallBack = null)
        {
            int stack = currentStack;
            if (currentStack == 0)
            {
                button.interactable = false;
                stackText.text = "";
            }
            else if (currentStack == 1)
            {
                stackText.text = "1";
            }
            else if (currentStack > 1)
            {
                stackText.text = currentStack.ToString();
            }
            fillImage.fillAmount = 1;
            fillImage.gameObject.SetActive(true);
            fillImage.DOKill();
            fillImage.DOFillAmount(0, time)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
                {
                    button.interactable = true;
                    fillImage.gameObject.SetActive(false);
                    stack += 1;
                    if (stack == 0)
                    {
                        button.interactable = false;
                        stackText.text = "";
                    }
                    else if (stack == 1)
                    {
                        stackText.text = "1";
                    }
                    else if (stack > 1)
                    {
                        stackText.text = stack.ToString();
                    }
                    completeCallBack?.Invoke();
                })
            .OnUpdate(() =>
                {
                    text.text = (fillImage.fillAmount * time).ToString("0.0");
                });
            currentStack++;
        }

        public void SetCoolDown(float startNormalizeTime, float duration, ref int currentStack, Action completeCallBack = null)
        {
            if (startNormalizeTime < 0)
            {
                throw new System.Exception("StartNormalizeTime must be more than 1");
            }
            if (startNormalizeTime >= 1)
            {
                StopCoolDown();
                return;
            }
            int stack = currentStack;
            if (currentStack == 0)
            {
                button.interactable = false;
                stackText.text = "";
            }
            else if (currentStack == 1)
            {
                stackText.text = "1";
            }
            else if (currentStack > 1)
            {
                stackText.text = currentStack.ToString();
            }
            fillImage.fillAmount = startNormalizeTime;
            fillImage.gameObject.SetActive(true);
            fillImage.DOKill();
            fillImage.DOFillAmount(0, duration * startNormalizeTime)
                .OnComplete(() =>
                {
                    button.interactable = true;
                    fillImage.gameObject.SetActive(false);
                    stack += 1;
                    if (stack == 0)
                    {
                        button.interactable = false;
                        stackText.text = "";
                    }
                    else if (stack == 1)
                    {
                        stackText.text = "1";
                    }
                    else if (stack > 1)
                    {
                        stackText.text = stack.ToString();
                    }
                    completeCallBack?.Invoke();
                })
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    text.text = (fillImage.fillAmount * duration).ToString("0.0");
                });
            currentStack++;
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