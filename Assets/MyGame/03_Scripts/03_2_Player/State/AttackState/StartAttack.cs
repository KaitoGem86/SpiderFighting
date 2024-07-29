using Animancer;
using Core.GamePlay.Support;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(StartAttack), menuName = ("PlayerState/" + nameof(StartAttack)), order = 0)]
    public class StartAttack : BasePlayerAction
    {
        [SerializeField] private float _mediumRange = 2f;
        [SerializeField] private float _longRange = 5f;
        [SerializeField] private FindEnemyToAttack _findEnemyModule;
        private IHittedByPlayer _enemyTarget;
        private bool _isCanChangeNextAttack = false;
        private bool _isStartGoToEnemy = false;

        public override void Enter(ActionEnum actionBefore)
        {
            _currentTransitionIndex = GetTransition(actionBefore);
            _currentTransition = _dictPlayerAnimTransition[_fixedAnim ? ActionEnum.None : actionBefore][_currentTransitionIndex];
            _isCanChangeNextAttack = false;
            _playerController.SetMovementMode(_movementMode);
            if(_movementMode == MovementMode.None)
            {
                _playerController.CharacterMovement.rigidbody.useGravity = true;
                _playerController.CharacterMovement.rigidbody.isKinematic = false;
                _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                _playerController.CharacterMovement.rigidbody.velocity = _playerController.GlobalVelocity;
            }
            else{
                _playerController.CharacterMovement.rigidbody.useGravity = false;
                _playerController.CharacterMovement.rigidbody.isKinematic = true;
                _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                _playerController.SetVelocity(_playerController.GlobalVelocity);
            }
            _isStartGoToEnemy = false;
            _enemyTarget = _findEnemyModule.FindEnemyByDistance(_playerController.transform);
            if(_enemyTarget == null)
            {
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
            if(Vector3.Distance(_enemyTarget.TargetEnemy.position, _playerController.transform.position) > _mediumRange)
                StartAction();
            else
                KeepAction();
            _onAttack?.RegisterListener();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onAttack?.UnregisterListener();
            return base.Exit(actionAfter);
        }

        public override void Attack()
        {
            if(!_isCanChangeNextAttack) return;
            _stateContainer.ChangeAction(ActionEnum.Attack1);
        }

        public override void KeepAction()
        {
            Debug.Log("KeepAction");
            base.KeepAction();
        }

        public void StartGoToEnemy(){
            if(_isStartGoToEnemy) return;
            _isStartGoToEnemy = true;
            _playerController.PlayerDisplay.DORotate(Quaternion.LookRotation(_enemyTarget.TargetEnemy.position - _playerController.transform.position).eulerAngles, 0.5f);
            _playerController.transform.DOMove(_enemyTarget.TargetEnemy.transform.position + (_playerController.transform.position - _enemyTarget.TargetEnemy.transform.position).normalized * 0.8f, 1f).OnComplete(KeepAction);            
        }

        public override void ExitAction()
        {
            _stateContainer.ChangeAction(ActionEnum.Idle);
        }

        public void CanChangeToAttack(){
            _isCanChangeNextAttack = true;
        }

        protected override int GetTransition(ActionEnum actionBefore)
        {
            return base.GetTransition(actionBefore);
        }
    }
}