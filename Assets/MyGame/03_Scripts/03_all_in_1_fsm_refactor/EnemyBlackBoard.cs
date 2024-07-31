using UnityEngine;
using UnityEngine.AI;

namespace Core.Test.Player{
    public class EnemyBlackBoard : BlackBoard
    {
        public NavMeshAgent navMeshAgent;       
        public Transform target;
    }
}