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
            base.EnterState();
            _fsm.blackBoard.Character.AddForce((_fsm.blackBoard.Character.GetVelocity() + Vector3.up * 5), ForceMode.Impulse);
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
            base.FixedUpdate();
        }

        // private Vector3 JumpVelocityFromSwing()
        // {
        //     // var forward = _fsm.transform.forward;
        //     // var velocity = Vector3.Project(_fsm.blackBoard.GlobalVelocity, forward);
        //     // forward.y = 0;
        //     // forward = forward.normalized;
        //     // GetInput();
        //     // var input = _moveDirection;
        //     // _speedFromSwing = Mathf.Clamp(_speedFromSwing, 0, 40);
        //     // if (Vector3.Angle(_fsm.blackBoard.GlobalVelocity, forward) > 30)
        //     // {
        //     //     var tmp = (forward + input).normalized * _speedFromSwing + velocity + Vector3.up * _jumpVelocity;
        //     //     tmp *= 2;
        //     //     tmp.y = Mathf.Clamp(tmp.y, 20, 30);
        //     //     return tmp;
        //     // }
        //     // else
        //     // {
        //     //     var tmp = velocity + Vector3.up * _jumpVelocity;
        //     //     tmp.y = Mathf.Clamp(tmp.y, 20, 30);
        //     //     return tmp;
        //     // }

        // }
    }
}