using Animancer;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class JumpFromClimbState : InAirState<ClipTransition> {
        public override void EnterState()
        {
            base.EnterState();
            Physics.Raycast(_fsm.transform.position, _fsm.transform.forward, out var hit, _blackBoard.ClimbLayer);
            _fsm.blackBoard.Character.AddForce(Vector3.up * 10, ForceMode.Impulse);
            _fsm.blackBoard.Character.AddForce(hit.normal * 5, ForceMode.Impulse);
        }

        public void CompleteClimbing()
        {
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.FallingDown);
        }
    }
}