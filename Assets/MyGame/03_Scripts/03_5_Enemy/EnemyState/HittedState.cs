using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class HittedState : BaseEnemyState<ClipTransition>
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.navMeshAgent.isStopped = true;
            _blackBoard.navMeshAgent.ResetPath();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}