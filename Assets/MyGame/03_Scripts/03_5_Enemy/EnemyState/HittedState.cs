using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class HittedState : ClipTransitionState<EnemyBlackBoard>
    {
        public override void EnterState()
        {
            base.EnterState();
            _fsm.blackBoard.navMeshAgent.isStopped = true;
            _fsm.blackBoard.navMeshAgent.ResetPath();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public void ReturnToWaitAttack(){
            _fsm.ChangeAction(FSMState.WaitAttack);
        }
    }
}