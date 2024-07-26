using Animancer;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(AttackAction)), order = 0)]
    public class AttackAction : BasePlayerAction
    {
        private int _attackCount = -1;
        private bool _isCanChangeNextAttack = false;
        private bool _notContinueAttack = true;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _attackCount = 0;
        }

        public override void Enter(ActionEnum actionBefore)
        {
            _onAttack?.RegisterListener();
            _isCanChangeNextAttack = false;
            if (actionBefore == ActionEnum.Attack)
            {
                if (_attackCount == 2)
                {
                    _attackCount = Random.Range(3, 6);
                }
                else if (_attackCount > 2)
                {
                    _attackCount = 0;
                }
                else
                {
                    _attackCount++;
                }
            }
            else
            {
                _attackCount = 0;
            }
            base.Enter(actionBefore);
            _notContinueAttack = true;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onAttack?.UnregisterListener();
            return base.Exit(actionAfter);
        }

        public override void KeepAction()
        {
            if (_currentTransition.keepAnimation.Animations.Length == 0)
            {
                return;
            }
            _displayContainer.PlayAnimation(_currentTransition.keepAnimation, 0);
            _currentTransition.keepAnimation.State.Parameter = _attackCount;
        }

        public override void ExitAction()
        {
            if(!_notContinueAttack) return;
            base.ExitAction();
            _stateContainer.ChangeAction(ActionEnum.Idle);
        }

        protected override int GetTransition(ActionEnum actionBefore)
        {
            if (actionBefore == ActionEnum.Attack && _attackCount != 0)
            {
                return _currentTransitionIndex;
            }
            else
            {
                return base.GetTransition(actionBefore);
            }
        }

        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            _notContinueAttack = false;
            _stateContainer.ChangeAction(ActionEnum.Attack);
        }

        public void CanChangeToAttack()
        {
            _isCanChangeNextAttack = true;
        }

        public void InitAnimAtribute(){
            _isCanChangeNextAttack = false;
            _notContinueAttack = true;
        }
    }
}