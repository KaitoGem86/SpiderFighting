using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.MyPlayer
{
    public class UseGadgetState : ClipTransitionPlayerState
    {
        private int _currentGadget;
        private GadgetsController _gadgetsController;

        protected override void Awake()
        {
            base.Awake();
            _gadgetsController = _fsm.blackBoard.GadgetsController;
        }

        public override void EnterState()
        {
            if (_currentGadget == 2)
            {
                _fsm.ChangeAction(FSMState.Idle);
                return;
            }
            base.EnterState();
        }

        protected override int GetIndexTransition()
        {
            switch (_currentGadget)
            {
                case 0: return 0;
                case 1: return 1;
                case 2: return 2;
                default: return 0;
            }
        }

        public void OnUseGadget()
        {
            _gadgetsController.UseGadget();
        }

        public void CompleteUseGadget()
        {
            _fsm.ChangeAction(FSMState.Idle);
        }

        public void ChangeGadget(int index)
        {
            _currentGadget = index;
        }
    }
}