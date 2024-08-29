namespace Core.GamePlay.MyPlayer{
    public class HitState : ClipTransitionPlayerState{
        public override void EnterState()
        {
            _blackBoard.Character.useRootMotion = true;
            base.EnterState();
        }

        public void CompleteHit()
        {
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }

        public override void ExitState()
        {
            _blackBoard.Character.useRootMotion = false;
            base.ExitState();
        }
    }
}