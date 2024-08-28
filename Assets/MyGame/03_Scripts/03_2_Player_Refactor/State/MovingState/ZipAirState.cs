using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class ZipAirState : InAirState<ClipTransition>
    {

        public override void EnterState()
        {
            _blackBoard.leftSilk.Init();
            _blackBoard.CameraDefault.Priority = _blackBoard.defaultPriority;
            _blackBoard.CameraLerpInAir.Priority = _blackBoard.topPriority;
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

        public void ShootSilk()
        {
            Vector3 target = _blackBoard.CurrentPlayerModel.leftHand.position + _blackBoard.transform.forward * 50;
            _blackBoard.leftSilk.ShootSilkToTarget(_blackBoard.CurrentPlayerModel.leftHand, target, 0.3f);
        }

        public override void ExitState()
        {
            _blackBoard.Character.gravityScale = 3;
            _blackBoard.leftSilk.UnUseSilk();
            _blackBoard.CameraDefault.Priority = _blackBoard.topPriority;
            _blackBoard.CameraLerpInAir.Priority = _blackBoard.defaultPriority;
            base.ExitState();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Rotate();
        }
    }
}