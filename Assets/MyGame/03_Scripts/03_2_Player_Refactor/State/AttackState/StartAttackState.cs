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
        
        public override void EnterState()
        {
            _enemy = _fsm.blackBoard.FindEnemyToAttack.FindEnemyByDistance(_fsm.transform);
            if (_enemy == null){
                _fsm.ChangeAction(FSMState.Idle);
                return;
            }
            base.EnterState();
        }

        public void ApplyDamage(){
            _enemy.HittedByPlayer();
        }

        public void GoToEnemy(float time){
            _fsm.transform.DOMove(_enemy.TargetEnemy.position + (_fsm.transform.position - _enemy.TargetEnemy.transform.position).normalized * 1f, time);
            var forward = _enemy.TargetEnemy.position - _fsm.transform.position; forward.y = 0;
            _fsm.blackBoard.PlayerDisplay.DORotateQuaternion(Quaternion.LookRotation(forward), time);
        }

        public override void Attack()
        {
            _fsm.ChangeAction(FSMState.Attack);
        }

        public void CompleteAttack(){
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