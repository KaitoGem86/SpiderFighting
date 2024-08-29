using Animancer;
using Core.GamePlay.Support;
using DG.Tweening;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class VenomUltimateSkill : BaseEnemyState<ClipTransition>
    {
        public ClipTransitionSequence[] respone;
        private Vector3 _targetPos;

        public override void EnterState()
        {
            base.EnterState();
            _animancer.Animator.applyRootMotion = true;
            _targetPos = _blackBoard.targetPos;
            _blackBoard.navMeshAgent.ResetPath();
            _blackBoard.navMeshAgent.isStopped = true;
            _blackBoard.transform.DORotateQuaternion(Quaternion.LookRotation(_targetPos - _blackBoard.transform.position), 0.1f);
        }

        public override void ExitState()
        {
            _animancer.Animator.applyRootMotion = false;
            base.ExitState();
        }

        public void ApplyFirstDamage(int handIndex)
        {
            _blackBoard.weaponController.SetHandEnemy(handIndex == 0 ? _blackBoard.currentModel.leftHand : _blackBoard.currentModel.rightHand);
            _blackBoard.targetToAttack.TargetEnemy.GetComponent<IHitted>().ResponseClip = respone[2];
            _blackBoard.targetToAttack.TargetEnemy.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(_blackBoard.transform.position - _blackBoard.targetToAttack.TargetEnemy.position);
            _blackBoard.weaponController.OnWeaponAttack(_blackBoard.targetToAttack.TargetEnemy, FSMState.ResponeForSpecialSkill);
        }

        public void ApplyDamage(){
            _blackBoard.weaponController.OnWeaponAttack(_blackBoard.targetToAttack.TargetEnemy, FSMState.None);
        }

        public void FixedUpdate()
        {
            RotateToEnemy();
        }

        private void RotateToEnemy()
        {
            //_blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(_targetPos - _blackBoard.navMeshAgent.transform.position), Time.deltaTime * 20);
        }

        protected override int GetIndexTransition()
        {
            return 2;
        }

    }
}