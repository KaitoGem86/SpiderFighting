using DG.Tweening;
using UnityEngine;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using Animancer;
namespace Core.GamePlay.Enemy
{
    public class AIAttackState : BaseEnemyState<ClipTransition>
    {
        [SerializeField] private DefaultEvent _onAttack;
        [SerializeField] private DefaultEvent _onCompleteAttack;
        private Vector3 _targetPos;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.navMeshAgent.ResetPath();
            _blackBoard.isReadyToAttack = false;
            //_onAttack?.Raise();
            _targetPos = _blackBoard.targetPos;
        }

        public override void Update()
        {
            _targetPos = _blackBoard.targetPos;
            base.Update();
        }

        public void FixedUpdate()
        {
            RotateToEnemy();
        }

        public override void ExitState()
        {
            //_onCompleteAttack?.Raise();
            _blackBoard.attackDelayTime = 5f;
            base.ExitState();
        }

        private void RotateToEnemy()
        {
            _blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(_targetPos - _blackBoard.navMeshAgent.transform.position), Time.deltaTime * 2);
        }

        public void ApplyDamage()
        {
            Debug.Log("Apply Damage");
            _blackBoard.weaponController.OnWeaponAttack(_blackBoard.targetToAttack.TargetEnemy, FSMState.Hit);
        }
    }
}