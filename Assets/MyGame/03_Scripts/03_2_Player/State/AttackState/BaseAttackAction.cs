namespace Core.GamePlay.Player
{
    public class BaseAttackAction : BasePlayerAction
    {
        protected bool _isCanChangeNextAttack = false;
        protected bool _notContinueAttack = false;

        public override void Enter(ActionEnum actionBefore)
        {
            base.Enter(actionBefore);
            _onAttack?.RegisterListener();
            _isCanChangeNextAttack = false;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onAttack?.UnregisterListener();
            return base.Exit(actionAfter);
        }

        public override void ExitAction()
        {
            _stateContainer.ChangeAction(ActionEnum.Idle);

        }

        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            _notContinueAttack = false;
            _stateContainer.ChangeAction(ActionEnum.Attack);
        }

        public void CanChangeToAttack()
        {
            _isCanChangeNextAttack = true;
        }
    }
}