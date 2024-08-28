using Extensions.SystemGame.AIFSM;
using Core.GamePlay.Enemy;
using Animancer;

namespace Core.GamePlay.Enemy{
    public class DeadState : BaseEnemyState<ClipTransition> {
        public override void EnterState()
        {
            base.EnterState();
            
            _blackBoard.navMeshAgent.isStopped = true;
            _blackBoard.transform.SetParent(null);
            _blackBoard.onEnemyDead?.Invoke();
        }
    }
}