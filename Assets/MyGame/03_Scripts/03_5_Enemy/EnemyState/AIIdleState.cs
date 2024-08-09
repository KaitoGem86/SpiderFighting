using UnityEngine;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class AIIdleState : ClipTransitionState<EnemyBlackBoard>
    {
        public override void EnterState()
        {
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