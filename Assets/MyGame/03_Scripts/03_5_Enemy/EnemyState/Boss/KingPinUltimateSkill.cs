using System.Collections;
using Animancer;
using Core.GamePlay.MyPlayer;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class KingPinUltimateSkill : BaseEnemyState<ClipTransitionSequence>{
        public ClipTransitionSequence _forwardResponse;
        public ClipTransitionSequence _backwardResponse;
        [SerializeField] private float _attackRange;
        private Vector3 _targetPos;
        private bool _isAttack;
        private Vector3 _direction;
        private bool _isMoving;

        public override void EnterState()
        {
            _isAttack = false;
            //_blackBoard.animancerComponent.Animator.applyRootMotion = true;
            base.EnterState();
            _targetPos = _blackBoard.targetPos;
            _direction = _targetPos - _blackBoard.transform.position;
            _isMoving = false;
        }

        public override void ExitState()
        {
            //_blackBoard.animancerComponent.Animator.applyRootMotion = false;
            base.ExitState();
        }

        public override void Update(){
            base.Update();
            //_targetPos = _blackBoard.targetPos;
            if(!_isMoving) return;
            _blackBoard.transform.position += _direction.normalized * Time.deltaTime * 10;
            if(Vector3.Distance(_targetPos, _blackBoard.transform.position) < _attackRange && !_isAttack){
                _isAttack = true;
                _blackBoard.targetToAttack.TargetEnemy.GetComponent<IHitted>().ResponseClip = GetResponse(_blackBoard.targetToAttack.TargetEnemy);
                _blackBoard.weaponController.OnWeaponAttack(_blackBoard.targetToAttack.TargetEnemy, FSMState.ResponeForSpecialSkill);
            }
        }

        private IEnumerator Rotate(int time){
            while (time > 0){
                RotateToEnemy();
                time--;
                yield return new WaitForEndOfFrame();
            }
        }

        private void RotateToEnemy()
        {
            //_blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(_targetPos - _blackBoard.navMeshAgent.transform.position), Time.deltaTime * 20);
        }

        private ClipTransitionSequence GetResponse(Transform target){
            var angle = Vector3.SignedAngle(target.forward, _blackBoard.transform.position - target.position, Vector3.up);
            if (angle > 90)
                return _forwardResponse;
            if (angle < -90)
                return _forwardResponse;
            return _backwardResponse;
        }

        public bool IsMoving { get => _isMoving; set => _isMoving = value; }
    }
}