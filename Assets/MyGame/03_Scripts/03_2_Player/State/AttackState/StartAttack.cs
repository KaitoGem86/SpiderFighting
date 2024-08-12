using Core.GamePlay.Support;
using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(StartAttack), menuName = ("PlayerState/" + nameof(StartAttack)), order = 0)]
    public class StartAttack : BasePlayerAction
    {
        [SerializeField] private float _mediumRange = 2f;
        [SerializeField] private float _longRange = 5f;
        [SerializeField] private FindEnemyToAttack _findEnemyModule;
        [SerializeField] private ShootSilk _shootSilk;
        private float _distanceToEnemy;
        private IHitted _enemyTarget;
        private bool _isCanChangeNextAttack = false;
        private bool _isStartGoToEnemy = false;

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _shootSilk.Init();
        }

        public override void Enter(ActionEnum actionBefore)
        {
            _isStartGoToEnemy = false;
            _enemyTarget = _findEnemyModule.FindEnemyByDistance(_playerController.transform);
            if (_enemyTarget == null)
            {
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
            _distanceToEnemy = Vector3.Distance(_enemyTarget.TargetEnemy.position, _playerController.transform.position);
            _isCanChangeNextAttack = false;
            _playerController.useRootMotion = true;

            base.Enter(actionBefore);

            _onAttack?.RegisterListener();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _playerController.useRootMotion = false;
            _onAttack?.UnregisterListener();
            return base.Exit(actionAfter);
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            _playerController.PlayerDisplay.rotation = Quaternion.Slerp(_playerController.PlayerDisplay.rotation, Quaternion.LookRotation(_enemyTarget.TargetEnemy.position - _playerController.PlayerDisplay.position), 0.2f);
        }

        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            _stateContainer.ChangeAction(ActionEnum.Attack1);
        }

        public void StartGoToEnemy(float time)
        {
            if (_isStartGoToEnemy) return;
            _isStartGoToEnemy = true;
            _playerController.PlayerDisplay.DORotate(Quaternion.LookRotation(_enemyTarget.TargetEnemy.position - _playerController.transform.position).eulerAngles, 0.2f);
            _playerController.transform.DOMove(_enemyTarget.TargetEnemy.transform.position + (_playerController.transform.position - _enemyTarget.TargetEnemy.transform.position).normalized * 1f, time);
        }

        public void RotateToTarget()
        {
            _playerController.PlayerDisplay.DORotate(Quaternion.LookRotation(_enemyTarget.TargetEnemy.position - _playerController.transform.position).eulerAngles, 0.2f);
        }

        public override void ExitAction()
        {
            _stateContainer.ChangeAction(ActionEnum.Idle);
        }

        public void CanChangeToAttack()
        {
            _isCanChangeNextAttack = true;
        }

        public void AttackEnemy()
        {
            _enemyTarget?.HittedByPlayer(Extensions.SystemGame.AIFSM.FSMState.Hit);
        }

        public void ShootSilk()
        {
            _shootSilk.ShootSilkToTarget(_playerController.rightHand, _enemyTarget.TargetEnemy.position, 0.15f);
        }

        protected override int GetTransition(ActionEnum actionBefore)
        {
            if (!_playerController.IsOnGround()) return 2;
            if (_distanceToEnemy > _longRange) return 2;
            if (_distanceToEnemy > _mediumRange) return 1;
            return 0;
        }
    }
}