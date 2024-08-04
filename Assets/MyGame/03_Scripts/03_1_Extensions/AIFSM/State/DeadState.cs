using System.Collections;
using Core.GamePlay.Enemy;

namespace Extensions.SystemGame.AIFSM{
    public class DeadState : ClipTransitionState {
        public override void EnterState()
        {
            base.EnterState();
        }

        public void CompleteDead(){
            var enemyController = (EnemyController) _fsm;
            enemyController.EnemySO.DespawnObject(_fsm.gameObject);
        }
    }
}