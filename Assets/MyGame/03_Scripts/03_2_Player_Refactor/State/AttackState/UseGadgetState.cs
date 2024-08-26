using Extensions.SystemGame.AIFSM;
using UnityEngine;
using DG.Tweening;


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
            _currentGadget = _blackBoard.PlayerData.playerSerializeData.gadgetIndex;
            if (_currentGadget == 2)
            {
                _fsm.ChangeAction(FSMState.Idle);
                return;
            }
            RotateToTarget();
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

        private void RotateToTarget()
        {
            //var forward = _fsm.blackBoard.FindEnemyToAttack.FindEnemyByDistance(_fsm.transform).TargetEnemy.position - _fsm.transform.position; forward.y = 0;
            var target = _fsm.blackBoard.FindEnemyToAttack.FindEnemyByDistance(_fsm.transform, false);
            if (target == null) return;
            _fsm.transform.DOLookAt(target.TargetEnemy.position, 0.2f);
        }
    }
}