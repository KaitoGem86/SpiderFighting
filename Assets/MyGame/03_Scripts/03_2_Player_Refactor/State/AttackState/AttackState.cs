using Animancer;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class AttackState : LinearMixerTransitionPlayerState
    {
        protected bool _isCanChangeNextAttack = false;
        private int _maxHitInCombo = 0;
        private int _currentHitInCombo = 0;
        private IHitted _enemy;


        protected override void Awake()
        {

            base.Awake();
            _currentHitInCombo = -1;
        }

        public override void EnterState()
        {
            _enemy = _fsm.blackBoard.FindEnemyToAttack.FindEnemyByDistance(_fsm.transform);
            
            if (_enemy!= null && Vector3.Distance(_enemy.TargetEnemy.position, _fsm.transform.position) > 2f)
            {
                _fsm.ChangeAction(FSMState.StartAttack);
                return;
            }
            base.EnterState();
            if (_currentHitInCombo == -1)
            {
                _maxHitInCombo = _transition.Animations.Length;
            }
            _currentHitInCombo++;
            _transition.State.Parameter = _currentHitInCombo;
            _isCanChangeNextAttack = false;
        }

        public void LateUpdate()
        {   
            var forward = _enemy != null  ? _enemy.TargetEnemy.position - _fsm.blackBoard.PlayerDisplay.position : _fsm.blackBoard.PlayerDisplay.forward; forward.y = 0;
            _fsm.blackBoard.PlayerDisplay.rotation = Quaternion.Slerp(_fsm.blackBoard.PlayerDisplay.rotation, Quaternion.LookRotation(forward), 0.2f);
        }


        public void CanChangeNextAttack()
        {
            _isCanChangeNextAttack = true;
        }

        public void ApplyDamage()
        {
            _enemy?.HittedByPlayer();
        }


        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            if (_currentHitInCombo < _maxHitInCombo - 1)
            {
                _fsm.ChangeAction(FSMState.Attack);
            }
            else
            {
                _currentHitInCombo = -1;
                _fsm.ChangeAction(FSMState.LastAttack);
            }
        }

        public void CompleteAttack()
        {
            _fsm.ChangeAction(FSMState.Idle);
        }

        protected override int GetIndexTransition()
        {
            if (_currentHitInCombo == -1)
            {
                return base.GetIndexTransition();
            }
            else
            {
                return _currentTransitionIndex;
            }
        }
    }
}