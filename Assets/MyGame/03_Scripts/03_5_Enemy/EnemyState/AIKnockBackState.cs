using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class AIKnockBackState : BaseEnemyState<ClipTransitionSequence>
    {
        public override void EnterState()
        {
            _fsm.blackBoard.navMeshAgent.ResetPath();
            _fsm.blackBoard.navMeshAgent.isStopped = true;
            _fsm.blackBoard.animancerComponent.Animator.applyRootMotion = true;
            base.EnterState();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void ExitState()
        {
            _fsm.blackBoard.animancerComponent.Animator.applyRootMotion = true;
            base.ExitState();
        }
    }
}