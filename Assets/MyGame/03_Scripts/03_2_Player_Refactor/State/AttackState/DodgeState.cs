using Animancer;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class DodgeState : LocalmotionState<LinearMixerTransition>
    {
        public override void EnterState()
        {
            _fsm.blackBoard.Character.useRootMotion = true;
            base.EnterState();
            GetInput();
            float angle = Vector3.SignedAngle(_fsm.blackBoard.PlayerDisplay.forward, _direction, Vector3.up);
            _transition.State.Parameter = angle;
        }

        public void CompleteDodge()
        {
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }

        public override void ExitState()
        {
            _fsm.blackBoard.Character.useRootMotion = false;
            base.ExitState();
        }
    }
}