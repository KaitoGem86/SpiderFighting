using Animancer;
using Extensions.SystemGame.AIFSM;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class LocalmotionState<T> : BasePlayerState<T> where T : ITransition
    {
        protected Vector3 _moveDirection;
        protected Vector3 _rotateDirection;
        protected Vector3 _direction;
        protected Vector3 _surfaceNormal;
        protected Transform _cameraTransform;
        protected Collider _groundCollider;

        protected override void Awake(){
            base.Awake();
            _cameraTransform = _fsm.blackBoard.CameraTransform;
        }

        protected virtual void Move()
        {
            _fsm.blackBoard.Character.SetMovementDirection(_moveDirection * _speed);
        }

        protected virtual void Rotate()
        {
            if (_rotateDirection == Vector3.zero) return;
            Quaternion targetRotation = Quaternion.LookRotation(_rotateDirection);
            _fsm.blackBoard.CurrentPlayerModel.PlayerDisplay.rotation = Quaternion.Slerp(_fsm.blackBoard.CurrentPlayerModel.PlayerDisplay.rotation, targetRotation, Time.deltaTime * 10);
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