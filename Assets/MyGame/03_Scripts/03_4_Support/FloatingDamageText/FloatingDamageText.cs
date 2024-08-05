using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class FloatingDamageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private FloatingDamageTextSO _soContainer;
        private float _timeAnim;

        public void OnSpawn(float dame, FloatingDamageTextSO soContainer)
        {
            _text.text = dame.ToString();
            _timeAnim = soContainer.timeToHide - 0.05f;
            _soContainer = soContainer;
            FloatingAnim();
            StartCoroutine(WaitToHideDamageText());
        }

        private void FixedUpdate()
        {
            var forward = Camera.main.transform.forward;
            forward.y = 0;
            forward.Normalize();
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(-forward), Time.fixedDeltaTime * 5);
        }

        private IEnumerator WaitToHideDamageText()
        {
            yield return new WaitForSeconds(_soContainer.timeToHide);
            _soContainer.DespawnObject(this.gameObject);
        }

        private void FloatingAnim()
        {
            this.transform.DOJump(this.transform.position + new Vector3(Random.Range(-1, 1), -1, 0), 1, 1, _timeAnim)
                .SetEase(Ease.OutQuad);
        }
    }
}