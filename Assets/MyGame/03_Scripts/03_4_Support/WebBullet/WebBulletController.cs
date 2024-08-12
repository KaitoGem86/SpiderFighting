using System.Collections;
using UnityEngine;

namespace Core.GamePlay.Support{
    public class WebBulletController : MonoBehaviour{
        [SerializeField] private Rigidbody _rb;
        private WebBulletSO _webBulletSO; 
        private bool _isMoving = false;
        private Vector3 _direction;

        public void Shoot(WebBulletSO so, Transform origin, IHitted targetEnemy){
            _webBulletSO = so;
            _isMoving = true;
            this.transform.position = origin.position;
            _direction = targetEnemy.TargetEnemy.position + Vector3.up - origin.position;
            StartCoroutine(Despawn());
        }

        public void Shoot(WebBulletSO so, Transform origin, Vector3 direction){
            _webBulletSO = so;
            _isMoving = true;
            this.transform.position = origin.position;
            _direction = direction;
            StartCoroutine(Despawn());
        }

        private void LateUpdate(){
            if(!_isMoving) return;
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + _direction, Time.deltaTime * 100);
        }

        private void OnTriggerEnter(Collider other){
            Debug.Log("OnTriggerEnter" + other.name);
            _isMoving = false;
            _webBulletSO.DespawnObject(this.gameObject);
            var enemy = other.GetComponent<IHitted>();
            if (enemy != null){
                enemy.HittedByPlayer(Extensions.SystemGame.AIFSM.FSMState.StunLock);
            }
        }

        private IEnumerator Despawn(){
            yield return new WaitForSeconds(10);
            _isMoving = false;
            _webBulletSO.DespawnObject(this.gameObject);
        }
    }
}