using DG.Tweening;
using UnityEngine;

namespace Extensions.SystemGame.AIFSM
{
    [CreateAssetMenu(menuName = "AIFSM/AIWaitAttackState")]
    public class AIWaitAttackState : BaseState
    {
        public override void EnterState(AIFSM fsm)
        {
            base.EnterState(fsm);
            fsm.blackBoard.animancer.Play(fsm.blackBoard.waitAttack);
            fsm.blackBoard.targetPosition = GetRandomPointOnCircle(fsm.blackBoard.enemyPosition, 5f);
            fsm.blackBoard.navMeshAgent.SetDestination(fsm.blackBoard.targetPosition);
        }

        public override void UpdateState(AIFSM fsm)
        {
            fsm.blackBoard.attackDelayTime -= Time.deltaTime;
            if (fsm.blackBoard.attackDelayTime <= 0){
                fsm.blackBoard.attackDelayTime = 5f;
                fsm.ChangeAction(AIState.Attack);
                return;
            }

            base.UpdateState(fsm);
            if (Vector3.Distance(fsm.blackBoard.navMeshAgent.transform.position, fsm.blackBoard.targetPosition) < 0.1f)
            {
                fsm.blackBoard.targetPosition = GetRandomPointOnCircle(fsm.blackBoard.enemyPosition, 5f);
                fsm.blackBoard.navMeshAgent.SetDestination(fsm.blackBoard.targetPosition);
            }
            PlayAnimationWithDirection(fsm);
            RotateDisplayWithVelocity(fsm);
        }

        public override void ExitState(AIFSM fsm)
        {
            base.ExitState(fsm);
        }

        public static Vector3 GetRandomPointOnCircle(Vector3 center, float radius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y; // Assuming the circle is in the XZ plane
            float z = center.z + radius * Mathf.Sin(angle);
            return new Vector3(x, y, z);
        }

        private void PlayAnimationWithDirection(AIFSM fsm)
        {
            Vector3 direction = (fsm.blackBoard.enemyPosition - fsm.blackBoard.navMeshAgent.transform.position).normalized;
            float angle = Vector3.SignedAngle(-fsm.blackBoard.navMeshAgent.velocity, -direction, Vector3.up);
            fsm.blackBoard.waitAttack.Transition.State.Parameter = angle;
        }

        private void RotateDisplayWithVelocity(AIFSM fsm)
        {
            Vector3 direction = fsm.blackBoard.enemyPosition - fsm.blackBoard.navMeshAgent.transform.position;
            fsm.blackBoard.navMeshAgent.transform.DOLookAt(fsm.blackBoard.enemyPosition, Time.deltaTime);
            Debug.DrawRay(fsm.blackBoard.navMeshAgent.transform.position, direction * 5, Color.red);
        }
    }
}