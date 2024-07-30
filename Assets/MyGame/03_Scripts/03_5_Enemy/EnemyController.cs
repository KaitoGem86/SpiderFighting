using Core.GamePlay.Support;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class EnemyController : MonoBehaviour, IHittedByPlayer{
        private EnemySO _soController;

        public void Init(EnemySO soConTroller){
            _soController = soConTroller;
        }

        public void HittedByPlayer()
        {
            Debug.Log("Hitted by player");
            _soController.DespawnObject(this.gameObject);
        }

        public Transform TargetEnemy { get => this.transform; }
    }
}