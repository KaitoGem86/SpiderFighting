using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer{
    public class PlayerController : FSM<PlayerBlackBoard>{

        protected override void Awake()
        {
            base.Awake();
            currentStateType = FSMState.None;
        }

        public override void ChangeAction(FSMState newState)
        {
            base.ChangeAction(newState);
            blackBoard.CurrentState = _currentState as IPlayerState;
        }

        public void OnCollided(ref CollisionResult collisionResult){
            
        }
    }
}