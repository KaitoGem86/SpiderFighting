using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Core.GamePlay.Support
{
    public class BlastController : MonoBehaviour
    {
        [SerializeField] GameObject _normal;
        [SerializeField] GameObject _explosion;
        [SerializeField] Rigidbody _rb;

        private BlastSO _so;

        void OnEnable()
        {
            _normal.SetActive(true);
            _explosion.SetActive(false);
        }

        public void Shoot(BlastSO so, Transform origin, IHitted hitted)
        {
            _so = so;
            this.transform.position = origin.position;
            _rb.DOJump(hitted.TargetEnemy.position, 3, 1, 1).OnComplete(Explosion);
            StartCoroutine(Despawn(10));
        }


        public void Shoot(BlastSO so, Transform origin, Vector3 direction)
        {
            _so = so;
            this.transform.position = origin.position;
            _rb.DOJump(origin.position + direction * 10, 3, 1, 1).OnComplete(Explosion);
            StartCoroutine(Despawn(10));
        }

        public void Explosion()
        {
            _explosion.SetActive(true);
            _normal.SetActive(false);
            StartCoroutine(Despawn(3));
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;
            Explosion();
        }

        private IEnumerator Despawn(float time)
        {
            yield return new WaitForSeconds(time);
            _so.DespawnObject(this.gameObject);
        }
    }
}