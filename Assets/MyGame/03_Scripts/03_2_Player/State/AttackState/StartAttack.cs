using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(StartAttack), menuName = ("PlayerState/" + nameof(StartAttack)), order = 0)]
    public class StartAttack : BasePlayerAction
    {
        [SerializeField] private float _distanceThreshold = 2f;
        private Transform _enemyTarget;
        private bool _isCanChangeNextAttack = false;

        private Transform GetEnemyTarget(){
            return _playerController.TestEnemy;
        }

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
            _enemyTarget = GetEnemyTarget();
            if(Vector3.Distance(_enemyTarget.position, _playerController.transform.position) > _distanceThreshold)
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
            _stateContainer.ChangeAction(ActionEnum.Attack);
        }

        public void StartGoToEnemy(){
            _playerController.transform.DOMove(_enemyTarget.position, 1f).OnComplete(KeepAction);            
        }

        public override void ExitAction()
        {
            _stateContainer.ChangeAction(ActionEnum.Idle);
        }

        public void CanChangeToAttack(){
            _isCanChangeNextAttack = true;
        }
    }
}