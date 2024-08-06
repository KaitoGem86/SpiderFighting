using Animancer;
using Extensions.SystemGame.AIFSM;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class StopMovingState : LocalmotionState<ClipTransition>{
        [SerializeField] private float _damping = 0f;
        private Vector3 _remainMoveDirection = Vector3.zero;
        private float _defaultSpeed;

        public override void EnterState()
        {
            base.EnterState();
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
            base.Update();
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

        public void LateUpdate()
        {
            _speed = Mathf.Lerp(_speed, 0, _damping );
            if (_speed < 0.1f)
            {
                _speed = 0;
                _fsm.ChangeAction(FSMState.Idle);
            }
            _moveDirection = _remainMoveDirection;
            Move();
            _remainMoveDirection = Vector3.Lerp(_remainMoveDirection, Vector3.zero, _damping * Time.deltaTime);
        }
    }
}