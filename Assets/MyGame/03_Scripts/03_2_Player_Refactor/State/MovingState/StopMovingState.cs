using Animancer;
using Extensions.SystemGame.AIFSM;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class StopMovingState : LocalmotionState<ClipTransition>
    {
        [SerializeField] private float _damping = 0f;
        private Vector3 _remainMoveDirection = Vector3.zero;
        private float _defaultSpeed;

        public override void EnterState()
        {
            base.EnterState();
            _remainMoveDirection = _fsm.blackBoard.Character.GetVelocity();
            _fsm.blackBoard.Character.useRootMotion = true;
            _defaultSpeed = _speed;
        }

        public override void ExitState()
        {
            base.ExitState();
            _fsm.blackBoard.Character.useRootMotion = false;
            _speed = _defaultSpeed;
        }

        public override void Update()
        {
            if (!_fsm.blackBoard.Character.IsOnGround())
            {
                _fsm.ChangeAction(FSMState.FallingDown);
                return;
            }
            GetInput();
            if (_moveDirection.magnitude > 0.1f)
            {
                _fsm.ChangeAction(FSMState.Moving);
                return;
            }
            if (InputManager.instance.jump)
            {
                _fsm.ChangeAction(FSMState.Jumping);
                return;
            }
        }

        public void FixedUpdate()
        {
            _speed = Mathf.Lerp(_speed, 0, _damping);
            if (_speed < 0.1f)
            {
                _speed = 0;

                Move();
                _fsm.ChangeAction(FSMState.Idle);

                return;
            }
            _moveDirection = _remainMoveDirection;
            Move();
            _remainMoveDirection = Vector3.Lerp(_remainMoveDirection, Vector3.zero, _damping * Time.deltaTime);
        }

    }
}