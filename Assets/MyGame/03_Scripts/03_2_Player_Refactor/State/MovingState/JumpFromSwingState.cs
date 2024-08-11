using UnityEngine;
using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer
{
    public class JumpFromSwingState : InAirState<ClipTransition>
    {
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
    }
}