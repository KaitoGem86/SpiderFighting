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
            _enemy = _findEnemyModule.FindEnemyByDistance(_playerController.transform);
            if(_enemy == null)
            {
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
            if(Vector3.Distance(_enemy.TargetEnemy.position, _playerController.transform.position) > 2f)
            {
                _stateContainer.ChangeAction(ActionEnum.StartAttack);
                return;
            }
            base.Enter(actionBefore);
            _onAttack?.RegisterListener();
            _isCanChangeNextAttack = false;
            _playerController.useRootMotion = true;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onAttack?.UnregisterListener();
            _playerController.useRootMotion = false;
            return base.Exit(actionAfter);
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            _playerController.PlayerDisplay.rotation = Quaternion.Slerp(_playerController.PlayerDisplay.rotation, Quaternion.LookRotation(_enemy.TargetEnemy.position - _playerController.PlayerDisplay.position), 0.2f);
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