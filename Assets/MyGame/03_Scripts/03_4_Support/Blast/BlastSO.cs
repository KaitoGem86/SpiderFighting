using UnityEngine;

using Core.SystemGame.Factory;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(fileName = "BlastSO", menuName = "ScriptableObjects/BlastSO", order = 1)]
    public class BlastSO : BaseSOWithPool {
        public void Spawn(Transform origin, IHitted hitted){
            var blast = SpawnObject().GetComponent<BlastController>();
            blast.Shoot(this, origin, hitted);
        }

        public void Spawn(Transform origin, Vector3 direction){
            var blast = SpawnObject().GetComponent<BlastController>();
            blast.Shoot(this, origin, direction);
        }
    }
}