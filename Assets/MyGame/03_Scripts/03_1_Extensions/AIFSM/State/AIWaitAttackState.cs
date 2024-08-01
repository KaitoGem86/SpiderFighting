using UnityEngine;
using UnityEngine.AI;

namespace Extensions.SystemGame.AIFSM{
    [CreateAssetMenu(menuName = "AIFSM/AIWaitAttackState")]
    public class AIWaitAttackState : BaseState{
        public override void EnterState(AIFSM fsm){
            base.EnterState(fsm);
            fsm.blackBoard.animancer.Play(fsm.blackBoard.idle);
        }

        public override void UpdateState(AIFSM fsm){
            base.UpdateState(fsm);
            fsm.blackBoard.navMeshAgent.SetDestination(GetNextPositionAroundTarget(fsm.blackBoard.enemyPosition, 5f));
        }

        public override void ExitState(AIFSM fsm){
            base.ExitState(fsm);
        }

        private Vector3 GetNextPositionAroundTarget(Vector3 targetPos, float radius){
            Vector3 randomPoint = Random.insideUnitSphere * radius;
            randomPoint += targetPos;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, radius, 1);
            return hit.position;
        }
    }
}