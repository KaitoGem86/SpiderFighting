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
            _fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
            _fsm.blackBoard.navMeshAgent.SetDestination(_fsm.blackBoard.targetPosition);
            //RotateDisplayWithVelocity();
        }

        public void LateUpdate()
        {
            _fsm.blackBoard.navMeshAgent.SetDestination(_fsm.blackBoard.targetPosition);
            _transition.State.Parameter = _fsm.blackBoard.navMeshAgent.velocity.magnitude;
            _fsm.blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_fsm.blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(_fsm.blackBoard.targetPosition - _fsm.blackBoard.navMeshAgent.transform.position), Time.deltaTime * 5);
        }
    }
}