using Extensions.SystemGame.AIFSM;
using UnityEngine;
using Core.GamePlay.Support;

namespace Core.GamePlay.MyPlayer
{
    public class LastAttackState : ClipTransitionPlayerState
    {
        protected bool _isCanChangeNextAttack = false;
        private IHitted _enemy;

        public override void EnterState()
        {
            _fsm.blackBoard.GlobalVelocity = Vector3.zero;
            _enemy = _fsm.blackBoard.FindEnemyToAttack.FindEnemyByDistance(_fsm.transform);

            if (_enemy != null && Vector3.Distance(_enemy.TargetEnemy.position, _fsm.transform.position) > 2f)
            {
                _fsm.ChangeAction(FSMState.StartAttack);
                return;
            }
            _fsm.blackBoard.Character.useRootMotion = true;
            base.EnterState();
            _isCanChangeNextAttack = false;
        }

        public override void ExitState()
        {
            _fsm.blackBoard.Character.useRootMotion = false;
            base.ExitState();
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
            _enemy?.KnockBack();
        }

        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            _fsm.ChangeAction(FSMState.StartAttack);
        }

        public void CompleteAttack()
        {
            _fsm.ChangeAction(FSMState.Idle);
        }
    }
}