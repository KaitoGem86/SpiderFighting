using Animancer;
using UnityEngine;
using UnityEngine.AI;

namespace Extensions.SystemGame.AIFSM
{
    public class BlackBoard : MonoBehaviour
    {
        [Header("========= General =========")]
        public Vector3 defaultPosition;
        public Vector3 targetPosition;

        [Header("========= Animation =========")]
        public AnimancerComponent animancer;
        public AnimancerTransitionAsset idle;
        public AnimancerTransitionAsset walk;
        public AnimancerTransitionAsset attack;
        public LinearMixerTransitionAsset waitAttack;

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