using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    public class IdleState : ClipTransitionState<BlackBoard>
    {
        public override void EnterState()
        {
            Debug.Log("IdleState");
            base.EnterState();
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