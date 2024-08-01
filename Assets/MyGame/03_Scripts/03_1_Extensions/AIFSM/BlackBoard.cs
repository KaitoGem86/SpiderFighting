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

        [Header("========= Movement =========")]
        public NavMeshAgent navMeshAgent;
        public Transform target;
        public float elapsedTimeToChangeTarget = 1f;
        public bool isChasePlayer = false;

        public Vector3 enemyPosition => target.position;

        void Awake()
        {
            defaultPosition = transform.position;            
        }
    }
}