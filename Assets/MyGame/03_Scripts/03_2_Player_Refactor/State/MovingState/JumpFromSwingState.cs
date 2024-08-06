using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer{
    public class JumpFromSwingState : InAirState<ClipTransition>{
        public void CompleteJumpFromSwing(){
            _fsm.ChangeAction(FSMState.Dive);
        }
    }
}