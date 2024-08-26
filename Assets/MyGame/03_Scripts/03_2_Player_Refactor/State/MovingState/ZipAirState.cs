using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class ZipAirState : InAirState<ClipTransition>
    {

        public override void EnterState()
        {
            base.EnterState();
        }

        public void AddForce()
        {
            _blackBoard.Character.SetVelocity(Vector3.zero);
            _blackBoard.Character.gravityScale = 0;
            _blackBoard.Character.AddForce(_blackBoard.transform.forward * _speed, UnityEngine.ForceMode.Impulse);
        }

        public void CompleteZip()
        {
            _fsm.ChangeAction(FSMState.FallingDown);
        }

        public override void ExitState()
        {
            _blackBoard.Character.gravityScale = 3;
            base.ExitState();
        }
    }
}