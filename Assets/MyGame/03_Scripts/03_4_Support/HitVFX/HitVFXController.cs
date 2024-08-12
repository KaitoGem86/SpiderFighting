using System.Collections;
using UnityEngine;

namespace Core.GamePlay.Support{
    public class HitVFXController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hitVFXs;
        private int _currentIndex = 0;

        public void ShowHitVFX()
        {
            _hitVFXs[_currentIndex].SetActive(false);
            _currentIndex = Random.Range(0, _hitVFXs.Length);
            _hitVFXs[_currentIndex].SetActive(true);
            StartCoroutine(HideVFX());
        }

        private IEnumerator HideVFX()
        {
            yield return new WaitForSeconds(0.75f);
            _hitVFXs[_currentIndex].SetActive(false);
        }
    }
}