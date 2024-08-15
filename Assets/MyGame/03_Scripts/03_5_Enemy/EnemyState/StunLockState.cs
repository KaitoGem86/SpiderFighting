using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class StunLockState : BaseEnemyState<ClipTransitionSequence> {
        public override void EnterState()
        {
            _blackBoard.navMeshAgent.ResetPath();
            _blackBoard.navMeshAgent.isStopped = true;
            base.EnterState();
        }

    }
}