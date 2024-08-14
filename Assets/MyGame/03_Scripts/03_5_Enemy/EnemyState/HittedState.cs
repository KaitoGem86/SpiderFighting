using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class HittedState : BaseEnemyState<ClipTransition>
    {
        public override void EnterState()
        {
            base.EnterState();
            _fsm.blackBoard.navMeshAgent.isStopped = true;
            _fsm.blackBoard.navMeshAgent.ResetPath();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}