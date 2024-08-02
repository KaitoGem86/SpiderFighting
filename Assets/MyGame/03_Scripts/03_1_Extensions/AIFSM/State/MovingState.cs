using DG.Tweening;
using UnityEngine;

namespace Extensions.SystemGame.AIFSM
{
    public class MovingState : LinearMixerTransitionState
    {
        public override void EnterState()
        {
            Debug.Log("MovingState");
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
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        private void RotateDisplayWithVelocity()
        {
            _fsm.blackBoard.navMeshAgent.transform.DOLookAt(_fsm.blackBoard.targetPosition, Time.deltaTime);
        }
    }
}