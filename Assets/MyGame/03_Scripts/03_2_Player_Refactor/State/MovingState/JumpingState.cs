using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer{
    public class JumpingState : InAirState<ClipTransition>{
        public override void EnterState()
        {
            base.EnterState();
            _fsm.blackBoard.Character.Jump();
        }

        public void CompleteJump(){
            _fsm.ChangeAction(FSMState.FallingDown);
        }

        public override void ExitState()
        {
            _fsm.blackBoard.Character.StopJumping();
            base.ExitState();
        }
    }
}