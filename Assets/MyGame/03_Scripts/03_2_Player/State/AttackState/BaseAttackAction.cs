using Core.GamePlay.Support;
using UnityEngine;

namespace Core.GamePlay.Player
{
    public class BaseAttackAction : BasePlayerAction
    {
        [SerializeField] protected FindEnemyToAttack _findEnemyModule;
        protected bool _isCanChangeNextAttack = false;
        protected bool _notContinueAttack = false;
        protected IHitted _enemy;

        public override void Enter(ActionEnum actionBefore)
        {
            base.Enter(actionBefore);
            _onAttack?.RegisterListener();
            _isCanChangeNextAttack = false;
            _playerController.useRootMotion = true;
            _enemy = _findEnemyModule.FindEnemyByDistance(_playerController.transform);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onAttack?.UnregisterListener();
            _playerController.useRootMotion = false;
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

        public void AttackEnemy(){ 
            _enemy?.HittedByPlayer();
        }

        public void CanChangeToAttack()
        {
            _isCanChangeNextAttack = true;
        }
    }
}