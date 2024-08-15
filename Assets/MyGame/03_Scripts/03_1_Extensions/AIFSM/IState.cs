namespace Extensions.SystemGame.AIFSM{
    public interface IState{
        void EnterState();
        void ExitState();
        FSMState StateType { get;}
        bool CanChangeToItself { get; }
    }

    public interface IFSM {
        void ChangeAction(FSMState state);
    }
}