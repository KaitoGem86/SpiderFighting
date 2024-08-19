using System.Collections;
using Animancer;
using Core.GamePlay.MyPlayer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class BossUltimateSkill : BaseEnemyState<ClipTransitionSequence>{
        public ClipTransitionSequence _forwardResponse;
        public ClipTransitionSequence _backwardResponse;
        [SerializeField] private float _attackRange;
        private Vector3 _targetPos;
        private bool _isAttack;

        public override void EnterState()
        {
            _isAttack = false;
            _blackBoard.animancerComponent.Animator.applyRootMotion = true;
            base.EnterState();
            _targetPos = _blackBoard.targetPos;
        }

        public override void ExitState()
        {
            _blackBoard.animancerComponent.Animator.applyRootMotion = false;
            base.ExitState();
            StartCoroutine(Rotate(1));
        }

        public override void Update(){
            base.Update();
            _targetPos = _blackBoard.targetPos;
            Debug.Log("BossUltimateSkill " + Vector3.Distance(_targetPos, _blackBoard.transform.position));
            if(Vector3.Distance(_targetPos, _blackBoard.transform.position) < _attackRange && !_isAttack){
                _isAttack = true;
                _blackBoard.targetToAttack.TargetEnemy.GetComponent<PlayerBlackBoard>().ResponeSpecialSkillAnim = _forwardResponse;
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
            _blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(_targetPos - _blackBoard.navMeshAgent.transform.position), Time.deltaTime * 20);
        }
    }
}