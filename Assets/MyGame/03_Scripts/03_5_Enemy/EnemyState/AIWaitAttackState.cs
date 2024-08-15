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
            _blackBoard.targetPosition = GetRandomPointOnCircle(_blackBoard.targetPos, 5f);
            _blackBoard.navMeshAgent.SetDestination(_blackBoard.targetPosition);
            _blackBoard.navMeshAgent.speed = _moveSpeed;
            if (!_blackBoard.isReadyToAttack)
            {
                _blackBoard.isReadyToAttack = true;
            }
        }

        public override void Update()
        {
            base.Update();
            if (Vector3.Distance(_blackBoard.navMeshAgent.transform.position, _blackBoard.targetPosition) < 0.1f)
            {
                Debug.Log(_blackBoard.attackRange + " " + _blackBoard.sightRange);
                _blackBoard.targetPosition = GetRandomPointOnCircle(_blackBoard.targetPos, Random.Range(_blackBoard.attackRange, _blackBoard.sightRange));
                _blackBoard.navMeshAgent.SetDestination(_blackBoard.targetPosition);
            }
            PlayAnimationWithDirection();
            RotateDisplayWithVelocity();
        }

        public override void ExitState()
        {
            _blackBoard.navMeshAgent.ResetPath();
            base.ExitState();
        }

        public static Vector3 GetRandomPointOnCircle(Vector3 center, float radius)
        {
            Debug.Log(radius );
            float angle = Random.Range(0f, Mathf.PI * 2);
            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y; // Assuming the circle is in the XZ plane
            float z = center.z + radius * Mathf.Sin(angle);
            return new Vector3(x, y, z);
        }

        private void PlayAnimationWithDirection()
        {
            Vector3 direction = (_blackBoard.targetPos - _blackBoard.navMeshAgent.transform.position).normalized;
            float angle = Vector3.SignedAngle(-_blackBoard.navMeshAgent.velocity, -direction, Vector3.up);
            _transition.State.Parameter = angle;
        }

        private void RotateDisplayWithVelocity()
        {
            var forward = _blackBoard.targetPos - _blackBoard.navMeshAgent.transform.position;
            _blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime * 5);
        }
    }
}