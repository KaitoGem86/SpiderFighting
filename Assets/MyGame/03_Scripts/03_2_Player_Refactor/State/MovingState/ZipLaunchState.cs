using UnityEngine;
using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer
{
    public class ZipLaunchState : LocalmotionState<ClipTransition>
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.Character.AddForce(Vector3.up * 40 + _blackBoard.transform.forward * 20, ForceMode.Impulse);
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.Character.StopJumping();
        }

        public void CompleteLaunch(){
            _fsm.ChangeAction(FSMState.Dive);
        }
    }
}