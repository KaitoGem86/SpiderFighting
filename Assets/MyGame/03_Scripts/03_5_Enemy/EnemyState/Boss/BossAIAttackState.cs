namespace Core.GamePlay.Enemy
{
    public class BossAIAttackState : AIAttackState
    {

        protected override void Awake()
        {
            base.Awake();
            _currentTransitionIndex = 0;
        }

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.animancerComponent.Animator.applyRootMotion = true;
        }

        public override void ExitState()
        {
            _blackBoard.animancerComponent.Animator.applyRootMotion = false;
            base.ExitState();
        }

        protected override int GetIndexTransition()
        {
            return (_currentTransitionIndex + 1) % _transitions.Length;
        }
    }
}