namespace Core.GamePlay.MyPlayer{
    public class SpawnState : ClipTransitionPlayerState{
        public override void Update(){
            if(_fsm.blackBoard.Character.IsOnGround()){
                CompleteSpawn();
            }
        }

        public void CompleteSpawn(){
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }
    }
}