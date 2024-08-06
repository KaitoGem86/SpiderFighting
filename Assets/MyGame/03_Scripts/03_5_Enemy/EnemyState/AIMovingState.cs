using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class AIMovingState : LinearMixerTransitionState<EnemyBlackBoard>
    {
        public override void EnterState()
        {
            base.EnterState();
            _fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
            _fsm.blackBoard.navMeshAgent.SetDestination(_fsm.blackBoard.targetPosition);
            //RotateDisplayWithVelocity();
        }

        public override void Update()
        {
            _fsm.blackBoard.elapsedTimeToChangeTarget -= Time.deltaTime;
            if (_fsm.blackBoard.elapsedTimeToChangeTarget <= 0)
            {
                _fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
                if (_fsm.blackBoard.isChasePlayer)
                {
                    _fsm.blackBoard.targetPosition = _fsm.blackBoard.enemyPosition;
                }
                _fsm.blackBoard.navMeshAgent.SetDestination(_fsm.blackBoard.targetPosition);
                //RotateDisplayWithVelocity();
            }
            _transition.State.Parameter = _fsm.blackBoard.navMeshAgent.velocity.magnitude;
            base.Update();
            _fsm.blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_fsm.blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(_fsm.blackBoard.targetPosition - _fsm.blackBoard.navMeshAgent.transform.position), Time.deltaTime * 5);
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}