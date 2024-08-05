using UnityEngine;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
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