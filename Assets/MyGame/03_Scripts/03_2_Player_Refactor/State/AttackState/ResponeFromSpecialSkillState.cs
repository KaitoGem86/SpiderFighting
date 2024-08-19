namespace Core.GamePlay.MyPlayer{
    public class ResponeFromSpecialSkillState : ClipTransitionPlayerState{

        public override void EnterState()
        {
            this.gameObject.SetActive(true);
            _transition = _blackBoard.ResponeSpecialSkillAnim;
            var state = _animancer.Play(_transition );
            state.Time = 0;
        }

        public void CompleteRespone(){
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }
    }
}