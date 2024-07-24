using UnityEngine;
using Core.GamePlay.Player;
using Core.SystemGame;
using SFRemastered.InputSystem;

namespace Core.GamePlay
{
    public class LocalmotionAction : BasePlayerAction
    {
        protected float _speed;
        protected Vector3 _moveDirection;
        protected Vector3 _rotateDirection;
        protected Vector3 _direction;
        protected Vector3 _surfaceNormal;
        protected float _maxAngularSlopes;
        protected Transform _cameraTransform;
        protected Collider _groundCollider;

        public override void Init(PlayerController playerController, ActionEnum actionEnum) 
        {
            base.Init(playerController, actionEnum);
            _cameraTransform = playerController.CameraTransform;
            _maxAngularSlopes = playerController.CharacterMovement.slopeLimit;
        }

        protected virtual void Move(){
            _playerController.SetMovementDirection(_moveDirection * _speed);
        }

        protected virtual void Rotate()
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rotateDirection);
            _playerController.PlayerDisplay.rotation = Quaternion.Slerp(_playerController.PlayerDisplay.rotation, targetRotation, Time.deltaTime * 10);
        }

        protected virtual void GetInput()
        {
            var joyStick = InputManager.instance.move;
            var right = _cameraTransform.right;
            right.y = 0;
            var forward = _cameraTransform.forward;
            forward.y = 0;
            Vector3 x = joyStick.x * right.normalized;
            Vector3 y = joyStick.y * forward.normalized;
            _direction = x + y;

            _direction.y = 0;
            _moveDirection = _direction;
            _rotateDirection = _direction;
        }
    }
}