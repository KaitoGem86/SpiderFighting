using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine.AI;

namespace Core.GamePlay.Mission.Protected{
    public class ProtectedNPCBlackBoard : BlackBoard{
        public AnimancerComponent animancer;
        public bool isPlayer;
        public float hp;
        public NeedProtectedNPC controller;
        public NavMeshAgent navMeshAgent;
    }
}