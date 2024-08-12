using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class StunLockState : ClipTransitionSequenceState<EnemyBlackBoard> {
        public override void EnterState()
        {
            _fsm.blackBoard.navMeshAgent.ResetPath();
            _fsm.blackBoard.navMeshAgent.isStopped = true;
            base.EnterState();
        }

        public void CompleteStunLock(){
            _fsm.ChangeAction(FSMState.WaitAttack);
        }
    }
}