using Extensions.SystemGame.AIFSM;
using Core.GamePlay.Enemy;
using Animancer;

namespace Core.GamePlay.Enemy{
    public class DeadState : BaseEnemyState<ClipTransition> {
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