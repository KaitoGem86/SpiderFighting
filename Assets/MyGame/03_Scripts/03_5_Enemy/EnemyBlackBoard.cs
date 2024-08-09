using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;
using UnityEngine.AI;

namespace Core.GamePlay.Enemy
{
    public class EnemyBlackBoard : BlackBoard
    {
        [Header("========= General =========")]
        public Vector3 defaultPosition;
        public Vector3 targetPosition;
        public AnimancerComponent animancerComponent;


        [Header("========= Movement =========")]
        public NavMeshAgent navMeshAgent;
        public Transform target;
        public float elapsedTimeToChangeTarget = 1f;
        public bool isChasePlayer = false;

        [Header("========= Attack =========")]
        public float attackDelayTime = 5f;

        public Vector3 enemyPosition => target.position;

        void Awake()
        {
            defaultPosition = transform.position;
        }
    }
}