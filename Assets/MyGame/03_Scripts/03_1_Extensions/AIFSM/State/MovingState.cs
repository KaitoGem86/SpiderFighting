using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    [CreateAssetMenu(menuName = "AIFSM/MovingState")]
    public class MovingState : BaseState{
        public override void EnterState(AIFSM fsm){
            Debug.Log("MovingState");
            base.EnterState(fsm);
            fsm.blackBoard.animancer.Play(fsm.blackBoard.walk);
            fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
            fsm.blackBoard.navMeshAgent.SetDestination(fsm.blackBoard.targetPosition);
        }

        public override void UpdateState(AIFSM fsm){
            fsm.blackBoard.elapsedTimeToChangeTarget -= Time.deltaTime;
            if (fsm.blackBoard.elapsedTimeToChangeTarget <= 0){
                fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
                if (fsm.blackBoard.isChasePlayer){
                    fsm.blackBoard.targetPosition = fsm.blackBoard.enemyPosition;
                }
                fsm.blackBoard.navMeshAgent.SetDestination(fsm.blackBoard.targetPosition);
            }
            base.UpdateState(fsm);
        }

        public override void ExitState(AIFSM fsm){
            base.ExitState(fsm);
        }
    }
}