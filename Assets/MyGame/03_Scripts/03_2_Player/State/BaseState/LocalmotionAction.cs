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

        protected virtual void MoveInAir(){
            Vector3 targetPos = Vector3.Lerp(_playerController.transform.position, _playerController.transform.position + _moveDirection * _speed, Time.deltaTime);
            _playerController.CharacterMovement.Move(_moveDirection * _speed );
        }

        protected virtual void Rotate()
        {
            if (_rotateDirection == Vector3.zero) return;
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

        protected virtual (bool, float) RaycastCheckGround()
        {
            float checkRadius = 0.2f; // Adjust this value as needed
            RaycastHit hit;
            Physics.SphereCast(_playerController.transform.position + Vector3.up * 0.4f, checkRadius, Vector3.down, out hit, 100, 2);
            if (hit.collider != null && hit.distance < 0.4f)
            {
                _surfaceNormal = hit.normal;
                _groundCollider = hit.collider;
                return (true, hit.distance);
            }
            else
            {
                _surfaceNormal = hit.collider == null ? Vector3.zero : hit.normal;
                return (false, hit.collider == null ? 100 : hit.distance);
            }
        }
    }
}