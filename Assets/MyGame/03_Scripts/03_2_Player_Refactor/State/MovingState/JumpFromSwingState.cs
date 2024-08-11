using UnityEngine;
using Animancer;
using Extensions.SystemGame.AIFSM;
using Unity.VisualScripting;

namespace Core.GamePlay.MyPlayer
{
    public class JumpFromSwingState : InAirState<ClipTransition>
    {
        [SerializeField] private float _jumpVelocity;
        private float _speedFromSwing;

        public override void EnterState()
        {
            _fsm.blackBoard.GlobalVelocity = JumpVelocityFromSwing();
            base.EnterState();
            _fsm.blackBoard.Character.AddForce(_fsm.blackBoard.GlobalVelocity, ForceMode.Impulse);
        }

        public void CompleteJumpFromSwing()
        {
            _fsm.ChangeAction(FSMState.Dive);
        }

        public override void Update()
        {
            if (_fsm.blackBoard.Character.IsGrounded())
            {
                _fsm.ChangeAction(FSMState.Landing);
                return;
            }
            base.Update();
        }

        public override void FixedUpdate()
        {
            Move();
        }

        private Vector3 JumpVelocityFromSwing()
        {
            var velocity = _fsm.blackBoard.GlobalVelocity;
            velocity.y = 0;
            velocity = velocity.normalized;
            GetInput();
            var input = _moveDirection;
            _speedFromSwing = Mathf.Clamp(_speedFromSwing, 0, 40);
            if (Vector3.Angle(_fsm.blackBoard.GlobalVelocity, velocity) > 30)
            {
                var tmp = (velocity + input).normalized * _speedFromSwing + _fsm.blackBoard.GlobalVelocity + Vector3.up * _jumpVelocity;
                tmp.y = Mathf.Clamp(tmp.y, 20, 30);
                return tmp;
            }
            else
            {
                var tmp = _fsm.blackBoard.GlobalVelocity + Vector3.up * _jumpVelocity;
                tmp.y = Mathf.Clamp(tmp.y, 20, 30);
                return tmp;
            }
        }
    }
}