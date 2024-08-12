using Animancer;
using Core.GamePlay.Support;
using DG.Tweening;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class StartAttackState : LocalmotionState<ClipTransitionSequence>{
        [SerializeField] private float _timeToAttack;
        [SerializeField] private float _mediumRange;
        [SerializeField] private float _longRange;
        private IHitted _enemy;
        private bool _canChangeToAttack;

        
        public override void EnterState()
        {
            _enemy = _fsm.blackBoard.FindEnemyToAttack.FindEnemyByDistance(_fsm.transform);
            _fsm.blackBoard.GlobalVelocity = Vector3.zero;
            if (_enemy == null){
                _fsm.ChangeAction(FSMState.Attack);
                return;
            }
            base.EnterState();
            _canChangeToAttack = false;
        }

        public void ApplyDamage(){
            _enemy?.HittedByPlayer();
        }

        public void GoToEnemy(float time){
            _fsm.transform.DOMove(_enemy.TargetEnemy.position + (_fsm.transform.position - _enemy.TargetEnemy.transform.position).normalized * 1f, time);
            var forward = _enemy.TargetEnemy.position - _fsm.transform.position; forward.y = 0;
            _fsm.transform.DORotateQuaternion(Quaternion.LookRotation(forward), 0.05f);
        }

        public override void Attack()
        {
            if(!_canChangeToAttack) return;
            _fsm.ChangeAction(FSMState.Attack);
        }

        public void CanChangeToAttack(){
            _canChangeToAttack = true;
        }

        public void CompleteAttack(){
            _moveDirection = Vector3.zero;
            Move();
            _fsm.ChangeAction(FSMState.Idle);
        }

        protected override int GetIndexTransition()
        {
            float distance = Vector3.Distance(_fsm.transform.position, _enemy.TargetEnemy.position);
            if (distance <= _mediumRange){
                return 0;
            } else if (distance <= _longRange){
                return 1;
            }
            return 2;
        }
    }
}