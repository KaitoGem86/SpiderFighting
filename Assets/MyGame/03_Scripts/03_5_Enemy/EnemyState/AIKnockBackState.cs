using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class AIKnockBackState : BaseEnemyState<ClipTransitionSequence>
    {
        public override void EnterState()
        {
            _blackBoard.navMeshAgent.ResetPath();
            _blackBoard.navMeshAgent.isStopped = true;
            _blackBoard.animancerComponent.Animator.applyRootMotion = true;
            base.EnterState();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void ExitState()
        {
            _blackBoard.animancerComponent.Animator.applyRootMotion = true;
            base.ExitState();
        }
    }
}