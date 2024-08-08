using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(fileName = "WebBulletSO", menuName = "ScriptableObjects/WebBulletSO", order = 1)]
    public class WebBulletSO : BaseSOWithPool {
        public void Spawn(Transform origin, IHitted hitted){
            var webBullet = SpawnObject().GetComponent<WebBulletController>();
            webBullet.Shoot(this, origin, hitted);
        }

        public void Spawn(Transform origin, Vector3 direction){
            var webBullet = SpawnObject().GetComponent<WebBulletController>();
            webBullet.Shoot(this, origin, direction);
        }
    }
}