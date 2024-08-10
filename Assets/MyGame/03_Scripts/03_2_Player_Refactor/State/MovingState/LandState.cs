using Animancer;
using Extensions.SystemGame.AIFSM;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class LandState : LocalmotionState<LinearMixerTransition>
    {
        [SerializeField] private float _landingVelocityThreshold = 1f;
        [SerializeField] private float _damping = 1f;
        private Vector3 _remainMoveDirection = Vector3.zero;
        private bool _isCanChangeAction = false;
        private float _defaultSpeed;

        protected override void Awake()
        {
            base.Awake();
            _fsm.blackBoard.Character.useRootMotion = true;
            _defaultSpeed = _speed;
        }

        public override void EnterState()
        {
            _speed = _defaultSpeed;
            _isCanChangeAction = false;
            _fsm.blackBoard.Character.StopJumping();
            base.EnterState();
            var velocity = _fsm.blackBoard.Character.GetCharacterMovement().velocity.magnitude;
            _remainMoveDirection = _fsm.transform.forward * velocity;
            GetInput();
            if(_moveDirection.magnitude < 0.1f && velocity < _landingVelocityThreshold)
            {
                _transition.State.Parameter = 0;
                _remainMoveDirection = Vector3.zero;
            }
            else if (velocity < _landingVelocityThreshold)
            {
                _transition.State.Parameter = 1;
            }
            else
            {
                _transition.State.Parameter = 2;
            }
        }
        public override void Update()
        {
            if (!_isCanChangeAction)
            {
                return;
            }
            if (InputManager.instance.move.magnitude > 0.1f)
            {
                _fsm.ChangeAction(FSMState.Moving);
                return;
            }
            base.Update();
        }

        public void FixedUpdate()
        {
            _speed = Mathf.Lerp(_speed, 0, _damping);
            if (_speed < 0.1f)
            {
                _speed = 0;
                if (_isCanChangeAction)
                {
                    Move();
                    _fsm.ChangeAction(FSMState.Idle);
                }
                return;
            }
            _moveDirection = _remainMoveDirection;
            Move();
            _remainMoveDirection = Vector3.Lerp(_remainMoveDirection, Vector3.zero, _damping * Time.deltaTime);
        }

        public void CanChangeToAction()
        {
            _isCanChangeAction = true;
        }

        public void CompleteLand()
        {
            //_fsm.ChangeAction(FSMState.Idle);
        }

        public override void ExitState()
        {
            _fsm.blackBoard.Character.useRootMotion = false;
            base.ExitState();
        }
    }
}