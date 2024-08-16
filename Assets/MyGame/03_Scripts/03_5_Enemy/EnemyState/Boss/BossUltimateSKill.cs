using Animancer;

namespace Core.GamePlay.Enemy{
    public class BossUltimateSkill : BaseEnemyState<ClipTransitionSequence>{
        public override void EnterState()
        {
            _blackBoard.animancerComponent.Animator.applyRootMotion = true;
            base.EnterState();
        }

        public override void ExitState()
        {
            _blackBoard.animancerComponent.Animator.applyRootMotion = false;
            base.ExitState();
        }
    }
}