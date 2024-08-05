using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer{
    public class FallingState : InAirState<ClipTransition>{
        public override void EnterState()
        {
            this.gameObject.SetActive(true);
        }

        public override void Update(){
            if (_fsm.blackBoard.Character.IsGrounded()){
                _fsm.ChangeAction(FSMState.Idle);
            }
        }
    }
}