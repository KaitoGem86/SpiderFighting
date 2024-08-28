using UnityEngine;
using Extensions.SystemGame.AIFSM;
using Animancer;

namespace Core.GamePlay.Enemy{
    public class AIIdleState : BaseEnemyState<ClipTransition>
    {
        public override void EnterState()
        {
            base.EnterState();
            if(!_blackBoard.navMeshAgent.enabled) return;
            _blackBoard.navMeshAgent.isStopped = true;
            _blackBoard.navMeshAgent.ResetPath();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}