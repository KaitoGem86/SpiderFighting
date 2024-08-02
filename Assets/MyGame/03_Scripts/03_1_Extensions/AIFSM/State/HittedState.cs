namespace Extensions.SystemGame.AIFSM{
    public class HittedState : ClipTransitionState
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
            _fsm.ChangeAction(AIState.WaitAttack);
        }
    }
}