using Animancer;
using Core.SystemGame;
using DG.Tweening;
using EasyCharacterMovement;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(MovingAction)), order = 0)]
    public class MovingAction : LocalmotionAction
    {
        private Transform _checkWallPivot;

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            GetInput();
            _speed = _statManager.GetValue(Support.StatType.MoveSpeed).value;
            _checkWallPivot = _playerController.CheckWallPivot;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public override void Update()
        {
            if (InputManager.instance.jump)
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            if (!_playerController.CharacterMovement.isOnGround)
            {
                _stateContainer.ChangeAction(ActionEnum.FallingDown);
                return;
            }
            GetInput();
            if (_moveDirection.Equals(Vector3.zero))
            {
                OnDontMove();
            }
        }

        public override void LateUpdate()
        {
            Move();
            Rotate();
            _currentTransition.keepAnimation.State.Parameter = _moveDirection.magnitude * _speed;
        }

        protected virtual void OnDontMove()
        {
            _stateContainer.ChangeAction(ActionEnum.StopMoving);
        }

        public override void OnCollided(ref CollisionResult other)
        {
            if (_playerController.CharacterMovement.groundCollider == null) return;
            if (other.collider.gameObject == _playerController.CharacterMovement.groundCollider.gameObject) return;
            base.OnCollided(ref other);
            if (Physics.Raycast(_checkWallPivot.position, _playerController.PlayerDisplay.forward, out var hit, _playerController.GetRadius()))
            {
                if (Vector3.Angle(hit.normal, Vector3.up) > _playerController.CharacterMovement.slopeLimit)
                {
                    _stateContainer.SurfaceNormal = hit.normal;
                    _stateContainer.ChangeAction(ActionEnum.Climbing);
                }
            }
        }
    }
}