using UnityEngine;
using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer
{
    public class ZipLaunchState : InAirState<ClipTransition>
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.Character.Jump();
        }

        public void CompleteLaunch(){
            _fsm.ChangeAction(FSMState.Dive);
        }
    }
}