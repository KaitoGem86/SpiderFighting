using Animancer;

namespace Core.GamePlay.MyPlayer
{
    public class ResponeFromSpecialSkillState : BasePlayerState<ClipTransitionSequence>
    {

        public override void EnterState()
        {
            this.gameObject.SetActive(true);
            _blackBoard.Character.useRootMotion = true;
            _transition = _blackBoard.PlayerController.ResponseClip;
            _transition.AddEvent(1, true, CompleteRespone);
            AnimancerState state = _animancer.Play(_transition);
            state.Time = 0;
            _fsm.blackBoard.PlayerController.IsIgnore = true;
        }

        public void CompleteRespone()
        {
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }

        public override void ExitState()
        {
            _blackBoard.Character.useRootMotion = false;
            _fsm.blackBoard.PlayerController.IsIgnore = false;
            base.ExitState();
        }
    }
}