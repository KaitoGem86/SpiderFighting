namespace Core.GamePlay.MyPlayer{
    public class SpawnState : ClipTransitionPlayerState{
        public void CompleteSpawn(){
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }
    }
}