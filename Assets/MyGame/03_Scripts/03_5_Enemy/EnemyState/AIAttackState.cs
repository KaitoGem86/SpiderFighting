using DG.Tweening;
using UnityEngine;
using Extensions.SystemGame.AIFSM;
namespace Core.GamePlay.Enemy{
    public class AIAttackState : LinearMixerTransitionState<EnemyBlackBoard>{
        [SerializeField] private float _meleeRange;
        [SerializeField] private float _mediumRange;
        [SerializeField] private float _longRange;
        private Vector3 _targetPos;

        public override void EnterState(){
            if(Vector3.Distance(_fsm.blackBoard.navMeshAgent.transform.position, _fsm.blackBoard.target.position) > _longRange){
                _fsm.ChangeAction(FSMState.Moving);
                return;
            }
            base.EnterState();
            _fsm.blackBoard.navMeshAgent.ResetPath();
            _targetPos = _fsm.blackBoard.target.position - (_fsm.blackBoard.target.position - _fsm.blackBoard.navMeshAgent.transform.position).normalized * 1;
            SetParamWithDistance();
            MoveToTargetInTime(0.3f);
        }

        public override void Update(){
            base.Update();
            RotateToEnemy();
        }

        public override void ExitState(){
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
        }

        public void CompleteAttack(){
            _fsm.ChangeAction(FSMState.WaitAttack);
        }
    }
}