using System;
using Animancer;
using DG.Tweening.Plugins.Options;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{

    [CreateAssetMenu(fileName = nameof(ClimbingAction), menuName = ("PlayerState/" + nameof(ClimbingAction)), order = 0)]
    public class ClimbingAction : LocalmotionAction
    {

        [SerializeField] private LinearMixerTransition _climbingForwardTransition;
        [SerializeField] private LinearMixerTransition _climbingLeftTranstion;
        [SerializeField] private LinearMixerTransition _climbingRightTransition;
        [SerializeField] private ClipTransition _climbingUpTransition;
        [SerializeField] private ClipTransition _climbingUpLeftTransition;
        [SerializeField] private ClipTransition _climbingUpRightTransition;

        LinearMixerState _currentState;
        private bool _isEndClimbing = false;

        public override void Enter()
        {
            _currentState = (LinearMixerState)_displayContainer.PlayAnimation(_climbingForwardTransition);
            _speed = 8f;
            _playerController.gravity = Vector3.zero;
            _isEndClimbing = false;
        }

        public override void LateUpdate()
        {
            GetInput();
            if (_isEndClimbing)
            {
                Rotate();
                return;
            }
            base.LateUpdate();
            var angle = GetAngle(_direction, Vector3.ProjectOnPlane(Vector3.up, _surfaceNormal));
            if (angle > 45)
            {
                _currentState = (LinearMixerState)_displayContainer.PlayAnimation(_climbingRightTransition);
            }
            else if (angle < -45)
            {
                _currentState = (LinearMixerState)_displayContainer.PlayAnimation(_climbingLeftTranstion);
            }
            else
            {
                _currentState = (LinearMixerState)_displayContainer.PlayAnimation(_climbingForwardTransition);
            }
            _currentState.Parameter = Mathf.Min(_moveDirection.magnitude * _speed, 2);
            _rotateDirection = -_surfaceNormal;
            Rotate();
            MoveInAir();
        }

        public override void FixedUpdate()
        {
            if (_isEndClimbing)
            {
                return;
            }
            base.FixedUpdate();
        }

        protected override void GetInput()
        {
            var joyStick = InputManager.instance.move;
            var right = _playerController.PlayerDisplay.right;
            var up = _playerController.PlayerDisplay.up;

            Vector3 x = joyStick.x * right.normalized;
            Vector3 y = joyStick.y * up.normalized;
            _direction = x + y;
            GetSurfaceNormal();
            _direction = Vector3.ProjectOnPlane(_direction, _surfaceNormal);

            _moveDirection = _direction;
            _rotateDirection = _direction;
        }

        private void GetSurfaceNormal()
        {
            if (_isEndClimbing)
            {
                return;
            }
            RaycastHit hit;
            if (Physics.Raycast(_playerController.transform.position, -_stateContainer.SurfaceNormal, out hit, 100))
            {
                if (hit.distance < 0.8f)
                {
                    _surfaceNormal = hit.normal;
                    return;
                }
            }
            _isEndClimbing = true;
            EndClimbing();
        }

        private void EndClimbing()
        {
            _playerController.gravity = Vector3.down * 9.8f;
            var angle = GetAngle(_direction, Vector3.ProjectOnPlane(Vector3.up, _surfaceNormal));
            if (angle > 45)
            {
                _displayContainer.ApplyRootMotion(true);
                _state = _displayContainer.PlayAnimation(_climbingUpRightTransition);
                _state.Events = _climbingUpRightTransition.Events;
            }
            else if (angle < -45)
            {
                _displayContainer.ApplyRootMotion(true);
                _state = _displayContainer.PlayAnimation(_climbingUpLeftTransition);
                _state.Events = _climbingUpLeftTransition.Events;
            }
            else
            {
                _state = _displayContainer.PlayAnimation(_climbingUpTransition);
                _state.Events = _climbingUpTransition.Events;
            }
            _isEndClimbing = true;
        }

        private float GetAngle(Vector3 markVector, Vector3 targetVector)
        {
            float angle = Vector3.Angle(markVector, targetVector);
            Vector3 crossProduct = Vector3.Cross(markVector, targetVector);
            if (Vector3.Dot(crossProduct, _surfaceNormal) > 0)
            {
                angle = -angle;
            }
            return angle;
        }

        public override void OnCollisionStay(Collision collision)
        {
            base.OnCollisionStay(collision);
            _surfaceNormal = collision.contacts[0].normal;
        }

        public void FallingDown()
        {
            _displayContainer.ApplyRootMotion(false);
            _stateContainer.ChangeAction(ActionEnum.FallingDown);
        }
    }
}