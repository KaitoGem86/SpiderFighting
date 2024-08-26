using UnityEngine;
using UnityEngine.UI;

namespace Core.GamePlay.Support
{
    public class HPBarController : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;
        [SerializeField] private Image _lerpHpBar;
        [SerializeField] private bool _isInCanvas;
        [SerializeField] private GameObject _activeObject;

        public void Init(float currentHP, float maxHP)
        {
            _hpBar.fillAmount = currentHP / maxHP;
            _lerpHpBar.fillAmount = currentHP / maxHP;
        }

        public void SetHP(float currentHP, float maxHP)
        {
            _hpBar.fillAmount = currentHP / maxHP;
        }

        public void SetHP(float coeficient)
        {
            if(coeficient == -1) {
                _activeObject.SetActive(false);
                return;
            }
            _hpBar.fillAmount = coeficient;
        }

        public void Update()
        {
            if (_lerpHpBar.fillAmount != _hpBar.fillAmount)
                _lerpHpBar.fillAmount = Mathf.Lerp(_lerpHpBar.fillAmount, _hpBar.fillAmount, Time.deltaTime * 5);
        }

        void LateUpdate(){
            if (_isInCanvas)
                return;
            var forward = Camera.main.transform.forward;
            forward.y = 0;
            forward.Normalize();
            Debug.DrawRay(this.transform.position, Camera.main.transform.position - this.transform.position, Color.red);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(-forward), Time.deltaTime * 20);   
        }
    }
}