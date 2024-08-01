using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    [CreateAssetMenu(menuName = "AIFSM/MovingState")]
    public class MovingState : BaseState{
        public override void EnterState(AIFSM fsm){
            Debug.Log("MovingState");
            base.EnterState(fsm);
            fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
            fsm.blackBoard.navMeshAgent.SetDestination(fsm.blackBoard.target.position);
        }

        public override void UpdateState(AIFSM fsm){
            fsm.blackBoard.elapsedTimeToChangeTarget -= Time.deltaTime;
            if (fsm.blackBoard.elapsedTimeToChangeTarget <= 0){
                fsm.blackBoard.elapsedTimeToChangeTarget = 1f;
                fsm.blackBoard.navMeshAgent.SetDestination(fsm.blackBoard.target.position);
            }
            base.UpdateState(fsm);
        }

        public override void ExitState(AIFSM fsm){
            base.ExitState(fsm);
        }
    }
}