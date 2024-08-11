using Animancer;

namespace Core.GamePlay.MyPlayer{
    public class DiveState : InAirState<ClipTransitionSequence>
    {
        public override void Update()
        {
            if (_fsm.blackBoard.Character.IsGrounded())
            {
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Landing);
                return;
            }
            base.Update();
        }
    }
}