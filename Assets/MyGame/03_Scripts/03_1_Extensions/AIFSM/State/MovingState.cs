using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    public class MovingState : ClipTransitionState{
        public override void EnterState(){
            Debug.Log("MovingState");
            base.EnterState();
            _fsm.blackBoard.animancer.Play(_fsm.blackBoard.walk);
            _fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
            _fsm.blackBoard.navMeshAgent.SetDestination(_fsm.blackBoard.targetPosition);
        }

        public override void Update(){
            _fsm.blackBoard.elapsedTimeToChangeTarget -= Time.deltaTime;
            if (_fsm.blackBoard.elapsedTimeToChangeTarget <= 0){
                _fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
                if (_fsm.blackBoard.isChasePlayer){
                    _fsm.blackBoard.targetPosition = _fsm.blackBoard.enemyPosition;
                }
                _fsm.blackBoard.navMeshAgent.SetDestination(_fsm.blackBoard.targetPosition);
            }
            base.Update();
        }

        public override void ExitState(){
            base.ExitState();
        }
    }
}