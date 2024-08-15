using Animancer;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class AimAttackState : BaseEnemyState<ClipTransition>
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.navMeshAgent.ResetPath();
            _blackBoard.isReadyToAttack = false;
            _blackBoard.weaponController.OnAim(_blackBoard.targetToAttack.TargetEnemy);
        }

        public void LateUpdate()
        {
            _blackBoard.navMeshAgent.transform.rotation = Quaternion.Slerp(_blackBoard.navMeshAgent.transform.rotation, Quaternion.LookRotation(_blackBoard.targetToAttack.TargetEnemy.position - _blackBoard.navMeshAgent.transform.position), Time.deltaTime * 2);
        }
    }
}