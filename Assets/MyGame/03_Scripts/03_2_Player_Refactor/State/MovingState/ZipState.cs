using Animancer;

namespace Core.GamePlay.MyPlayer{
    public class ZipState : BasePlayerState<ClipTransitionSequence>{
        public void CompleteGroundZip(){
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }
    }
}