using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer{
    public class JumpingState : InAirState<ClipTransition>{
        public override void EnterState()
        {
            _fsm.blackBoard.Character.jumpImpulse = _fsm.blackBoard.PlayerData.playerStatSO.GetGlobalStat(Data.Stat.Player.PlayerStat.JumpHeight);
            base.EnterState();
            _fsm.blackBoard.Character.Jump();
        }

        public void CompleteJump(){
            _fsm.ChangeAction(FSMState.FallingDown);
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}