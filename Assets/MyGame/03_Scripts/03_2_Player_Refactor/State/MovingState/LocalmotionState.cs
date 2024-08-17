using Animancer;
using EasyCharacterMovement;
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

        protected override void Awake()
        {
            base.Awake();
            _cameraTransform = _fsm.blackBoard.CameraTransform;
        }

        protected virtual void Move()
        {
            if (_movementMode == MovementMode.None)
            {
                var rigidbody = _fsm.blackBoard.Character.GetCharacterMovement().rigidbody;
                rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, _moveDirection * _speed, Time.deltaTime * 10);
                //Rotate();
            }
            else
            {
                _fsm.blackBoard.Character.SetMovementDirection(_moveDirection * _speed);

            }
        }

        protected virtual void Rotate()
        {
            if (_rotateDirection == Vector3.zero) return;
            Quaternion targetRotation = Quaternion.LookRotation(_rotateDirection);
            _fsm.blackBoard.Character.GetCharacterMovement().rigidbody.rotation = Quaternion.Slerp(_fsm.blackBoard.Character.GetCharacterMovement().rotation, targetRotation, Time.deltaTime * 10);
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