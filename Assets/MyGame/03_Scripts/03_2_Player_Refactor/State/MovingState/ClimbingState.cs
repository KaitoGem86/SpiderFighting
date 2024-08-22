using Animancer;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class ClimbingState : LocalmotionState<LinearMixerTransition>
    {
        LinearMixerTransition _currentState;
        private bool _isEndClimbing = false;
        private bool _isCompleteStartClimbing = false;

        public override void EnterState()
        {
            _isEndClimbing = false;
            _isCompleteStartClimbing = false;
            base.EnterState();
            _fsm.blackBoard.Character.GetCharacterMovement().rigidbody.useGravity = false;
        }

        public override void Update()
        {
            // if (!_isCompleteStartClimbing)
            // {
            //     return;
            // }
            if (InputManager.instance.jump)
            {
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Jumping);
                return;
            }
            base.Update();
            GetInput();
        }


        public void FixedUpdate()
        {
            // if (!_isCompleteStartClimbing)
            // {
            //     return;
            // }
            if (_isEndClimbing)
            {
                Rotate();
                return;
            }
            var angle = GetAngle(_direction, Vector3.ProjectOnPlane(Vector3.up, _surfaceNormal));
            if (angle > 45)
            {
                _currentState = _transitions[2];
                _fsm.blackBoard.Animancer.Play(_currentState);
                if (angle > 90)
                {
                    _moveDirection = _fsm.transform.right;
                }
            }
            else if (angle < -45)
            {
                _currentState = _transitions[1];
                _fsm.blackBoard.Animancer.Play(_currentState);
                if (angle < -90)
                {
                    _moveDirection = -_fsm.transform.right;
                }
            }
            else
            {
                _currentState = _transitions[0];
                _fsm.blackBoard.Animancer.Play(_currentState);
            }
            _currentState.State.Parameter = Mathf.Min(_moveDirection.magnitude * _speed, 2);
            _rotateDirection = -_surfaceNormal;
            Rotate();
            Move();
        }

        protected override void Move()
        {
            _fsm.blackBoard.Character.GetCharacterMovement().rigidbody.velocity = _moveDirection * _speed;
        }

        protected override void GetInput()
        {
            var joyStick = InputManager.instance.move;
            var right = _fsm.transform.right;
            var up = _fsm.transform.up;

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
            if (Physics.Raycast(_fsm.blackBoard.CheckWallPivot.position, _fsm.transform.forward, out hit, 100))
            {
                if (hit.distance < 0.8f)
                {
                    _surfaceNormal = hit.normal;
                    return;
                }
                else
                {
                }
            }
            else
            {
            }
            _isEndClimbing = true;
            EndClimbing();
        }

        // private void StartClimbing()
        // {
        //     GetInput();
        //     var angle = GetAngle(_direction, Vector3.ProjectOnPlane(Vector3.up, _surfaceNormal));
        //     if (angle > 45 && _moveDirection.magnitude * _speed > 0.1f)
        //     {
        //         _state = _displayContainer.PlayAnimation(_startClimbingUpRightTransition);
        //     }
        //     else if (angle < -45 && _moveDirection.magnitude * _speed > 0.1f)
        //     {
        //         _state = _displayContainer.PlayAnimation(_startClimbingUpLeftTransition);
        //     }
        //     else
        //     {
        //         _state = _displayContainer.PlayAnimation(_startClimbingUpTransition);
        //     }
        // }

        private void EndClimbing()
        {
            Debug.Log("End Climbing");
            _isEndClimbing = true;
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.FallingDown);
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

        public void CompleteClimbing()
        {
            _isCompleteStartClimbing = true;
        }
    }
}