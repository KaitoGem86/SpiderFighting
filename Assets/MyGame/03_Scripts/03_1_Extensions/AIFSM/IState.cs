namespace Extensions.SystemGame.AIFSM{
    public interface IState{
        void EnterState();
        void ExitState();
        AIState StateType { get;}
        bool CanChangeToItself { get; }
    }
}