using UnityEngine;
using UnityEngine.UI;

namespace Core.GamePlay.Support
{
    public class HPBarController : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;
        [SerializeField] private Image _lerpHpBar;

        public void Init(float currentHP, float maxHP)
        {
            _hpBar.fillAmount = currentHP / maxHP;
            _lerpHpBar.fillAmount = currentHP / maxHP;
        }

        public void SetHP(float currentHP, float maxHP)
        {
            _hpBar.fillAmount = currentHP / maxHP;
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.J))
                SetHP(50, 100);

            if (_lerpHpBar.fillAmount != _hpBar.fillAmount)
                _lerpHpBar.fillAmount = Mathf.Lerp(_lerpHpBar.fillAmount, _hpBar.fillAmount, Time.deltaTime * 5);
        }
    }
}