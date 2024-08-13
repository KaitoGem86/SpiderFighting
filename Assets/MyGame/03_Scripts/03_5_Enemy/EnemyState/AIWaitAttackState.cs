using DG.Tweening;
using UnityEngine;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using Animancer;

namespace Core.GamePlay.Enemy
{
    public class AIWaitAttackState : BaseEnemyState<LinearMixerTransition>
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private DefaultEvent _onReadyToAttack;


        public override void EnterState()
        {
            base.EnterState();
            _fsm.blackBoard.targetPosition = GetRandomPointOnCircle(_fsm.blackBoard.enemyPosition, 5f);
            _fsm.blackBoard.navMeshAgent.SetDestination(_fsm.blackBoard.targetPosition);
            _fsm.blackBoard.navMeshAgent.speed = _moveSpeed;
            if (!_fsm.blackBoard.isReadyToAttack)
            {
                _onReadyToAttack?.Raise();
                _fsm.blackBoard.isReadyToAttack = true;
            }
        }

        public override void Update()
        {
            _fsm.blackBoard.attackDelayTime -= Time.deltaTime;

            base.Update();
            if (Vector3.Distance(_fsm.blackBoard.navMeshAgent.transform.position, _fsm.blackBoard.targetPosition) < 0.1f)
            {
                _fsm.blackBoard.targetPosition = GetRandomPointOnCircle(_fsm.blackBoard.enemyPosition, 5f);
                _fsm.blackBoard.navMeshAgent.SetDestination(_fsm.blackBoard.targetPosition);
            }
            PlayAnimationWithDirection();
            RotateDisplayWithVelocity();
        }

        public override void ExitState()
        {
            _fsm.blackBoard.navMeshAgent.ResetPath();
            base.ExitState();
        }

        public static Vector3 GetRandomPointOnCircle(Vector3 center, float radius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y; // Assuming the circle is in the XZ plane
            float z = center.z + radius * Mathf.Sin(angle);
            return new Vector3(x, y, z);
        }

        private void PlayAnimationWithDirection()
        {
            Vector3 direction = (_fsm.blackBoard.enemyPosition - _fsm.blackBoard.navMeshAgent.transform.position).normalized;
            float angle = Vector3.SignedAngle(-_fsm.blackBoard.navMeshAgent.velocity, -direction, Vector3.up);
            _transition.State.Parameter = angle;
        }

        private void RotateDisplayWithVelocity()
        {
            var forward = _fsm.blackBoard.target.position - _fsm.blackBoard.navMeshAgent.transform.position;
            _fsm.blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_fsm.blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime * 5);
        }
    }
}