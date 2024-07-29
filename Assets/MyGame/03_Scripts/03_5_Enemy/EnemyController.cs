using Core.GamePlay.Support;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class EnemyController : MonoBehaviour, IHittedByPlayer{
        public void HittedByPlayer()
        {
            Debug.Log("Hitted by player");
        }

        public Transform TargetEnemy { get => this.transform; }
    }
}