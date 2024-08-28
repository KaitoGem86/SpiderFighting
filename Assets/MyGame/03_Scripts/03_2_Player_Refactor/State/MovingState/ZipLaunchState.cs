using UnityEngine;
using Animancer;
using Extensions.SystemGame.AIFSM;
using EasyCharacterMovement;

namespace Core.GamePlay.MyPlayer
{
    public class ZipLaunchState : InAirState<ClipTransition>
    {
        private bool _isAfterLaunch = false;

        public override void EnterState()
        {
            base.EnterState();
            _isAfterLaunch = false;
            _blackBoard.Character.StopJumping();
            _blackBoard.Character.Jump();
            Launch();
        }

        public override void FixedUpdate()
        {
            //base.FixedUpdate();
            Rotate();
        }

        public void Launch()
        {
            Debug.Log("Launch");
            _isAfterLaunch = true;
            //_blackBoard.Character.GetCharacterMovement().rigidbody.AddForce(Vector3.up * 100 + _blackBoard.transform.forward * 60, ForceMode.Impulse);
            _blackBoard.Character.AddForce(Vector3.up * 30, ForceMode.Impulse);
            _blackBoard.Character.AddForce(_blackBoard.transform.forward * 30, ForceMode.Impulse);
        }

        public void CompleteLaunch()
        {
            _fsm.ChangeAction(FSMState.Dive);
        }

        public override void OnCollided(ref CollisionResult collision)
        {
            if (!_isAfterLaunch)
                return;
            base.OnCollided(ref collision);
        }

        public override void OnCollisionEnter(Collision collision)
        {
            if (!_isAfterLaunch)
                return;
            base.OnCollisionEnter(collision);
        }
    }
}