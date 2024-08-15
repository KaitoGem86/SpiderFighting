using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class AIMovingState : BaseEnemyState<LinearMixerTransition>
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.navMeshAgent.SetDestination(_blackBoard.targetPosition);
            //RotateDisplayWithVelocity();
        }

        public void LateUpdate()
        {
            _blackBoard.navMeshAgent.SetDestination(_blackBoard.targetPosition);
            _transition.State.Parameter = _blackBoard.navMeshAgent.velocity.magnitude;
            _blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(_blackBoard.targetPosition - _blackBoard.navMeshAgent.transform.position), Time.deltaTime * 5);
        }
    }
}