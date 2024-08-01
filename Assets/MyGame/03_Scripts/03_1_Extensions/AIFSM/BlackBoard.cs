using Animancer;
using UnityEngine;
using UnityEngine.AI;

namespace Extensions.SystemGame.AIFSM
{
    public class BlackBoard : MonoBehaviour
    {
        [Header("Animation")]
        public AnimancerComponent animancer;
        public AnimancerTransitionAsset idle;

        [Header("Movement")]
        public NavMeshAgent navMeshAgent;
        public Transform target;
        public float elapsedTimeToChangeTarget = 1f;

    }
}