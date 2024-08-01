using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    public class IdleState : ClipTransitionState
    {
        public override void EnterState()
        {
            Debug.Log("IdleState");
            base.EnterState();
            _fsm.blackBoard.animancer.Play(_fsm.blackBoard.idle);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}