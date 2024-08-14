using DG.Tweening;
using UnityEngine;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using Animancer;
namespace Core.GamePlay.Enemy{
    public class AIAttackState : BaseEnemyState<LinearMixerTransition>{
        [SerializeField] private float _meleeRange;
        [SerializeField] private float _mediumRange;
        [SerializeField] private float _longRange;
        [SerializeField] private DefaultEvent _onAttack;
        [SerializeField] private DefaultEvent _onCompleteAttack;
        private Vector3 _targetPos;

        public override void EnterState(){
            base.EnterState();
            _fsm.blackBoard.navMeshAgent.ResetPath();
            _fsm.blackBoard.isReadyToAttack = false;
            _onAttack?.Raise();
            _targetPos = _fsm.blackBoard.targetPos;
            //SetParamWithDistance();
            //MoveToTargetInTime(0.3f);
        }

        public override void Update(){
            _targetPos = _fsm.blackBoard.targetPos;
            base.Update();
            RotateToEnemy();
        }

        public override void ExitState(){
            _onCompleteAttack?.Raise();
            _fsm.blackBoard.attackDelayTime = 5f;
            base.ExitState();
        }

        private void SetParamWithDistance(){
            float distance = Vector3.Distance(_fsm.blackBoard.navMeshAgent.transform.position, _targetPos);
            if (distance <= _meleeRange){
                _transition.State.Parameter = 0;
            } else if (distance <= _mediumRange){
                _transition.State.Parameter = 1;
            } else if (distance <= _longRange){
                _transition.State.Parameter = 2;
            }
        }

        private void MoveToTargetInTime(float timer){
            _fsm.blackBoard.navMeshAgent.SetDestination(_targetPos);
            _fsm.blackBoard.navMeshAgent.speed = Vector3.Distance(_fsm.blackBoard.navMeshAgent.transform.position, _targetPos) / timer;
        }
        
        private void RotateToEnemy(){
            _fsm.blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_fsm.blackBoard.navMeshAgent.transform.rotation,Quaternion.LookRotation(_targetPos - _fsm.blackBoard.navMeshAgent.transform.position), Time.deltaTime * 2);
        }

        public void ApplyDamage(){
            Debug.Log("Apply Damage");
            _fsm.blackBoard.weaponController.OnWeaponAttack(_fsm.blackBoard.targetToAttack.TargetEnemy);
        }

        public void CompleteAttack(){
            _fsm.ChangeAction(FSMState.WaitAttack);
        }
    }
}