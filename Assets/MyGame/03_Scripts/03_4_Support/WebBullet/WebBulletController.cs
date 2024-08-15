using System.Collections;
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class WebBulletController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private TrailRenderer _trail;
        private WebBulletSO _webBulletSO;
        private bool _isMoving = false;
        private Vector3 _direction;
        private float _speed;

        public void Shoot(WebBulletSO so, Transform origin, IHitted targetEnemy)
        {
            _webBulletSO = so;
            _isMoving = true;
            this.transform.SetParent(null);
            _rb.position = origin.position;
            _direction = targetEnemy.TargetEnemy.position + Vector3.up - origin.position;
            StartCoroutine(Despawn());
            _speed = _webBulletSO.speed;
        }

        public void Shoot(WebBulletSO so, Transform origin, Vector3 direction)
        {
            _webBulletSO = so;
            _isMoving = true;
            this.transform.SetParent(null);
            _rb.position = origin.position;
            _direction = direction;
            StartCoroutine(Despawn());
            _speed = _webBulletSO.speed;
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;
            _trail.gameObject.SetActive(true);
            _rb.position = _rb.position + _direction.normalized * Time.fixedDeltaTime * _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            _isMoving = false;
            var enemy = other.GetComponent<IHitted>();
            if (enemy != null)
            {
                enemy.HittedByPlayer(_webBulletSO.hitState);
            }
            _trail.Clear();
            _trail.gameObject.SetActive(false);
            _webBulletSO.DespawnObject(this.gameObject);

        }

        private IEnumerator Despawn()
        {
            yield return new WaitForSeconds(10);
            _isMoving = false;
            _trail.Clear();
            _trail.gameObject.SetActive(false);
            _webBulletSO.DespawnObject(this.gameObject);
        }
    }
}