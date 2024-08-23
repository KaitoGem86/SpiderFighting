using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.UI{
    public class HitCounterAnim : MonoBehaviour{
        [SerializeField] private TMP_Text _hitCounterText;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _duration;


        public void OnEnable(){
            _hitCounterText.transform.DOLocalMoveY(0, _duration).SetEase(Ease.OutBounce);
            _hitCounterText.DOFade(1, _duration).OnComplete(() => {
                StartCoroutine(LifeTime());
            });
        }

        public void OnDisable(){
            _hitCounterText.DOKill();
            StopAllCoroutines();
            ResetText();
        }

        private IEnumerator LifeTime(){
            yield return new WaitForSeconds(_lifeTime);
            ResetText();
        }

        private void ResetText(){
            _hitCounterText.transform.localPosition = _offset;
            var color = _hitCounterText.color;
            color.a = 0;
            _hitCounterText.color = color;
        }
    }
}