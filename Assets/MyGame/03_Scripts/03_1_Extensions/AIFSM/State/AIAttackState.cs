using UnityEngine;
namespace Extensions.SystemGame.AIFSM{
    [CreateAssetMenu(menuName = "AIFSM/AttackState")]
    public class AIAttackState : BaseState{
        public override void EnterState(AIFSM fsm){
            base.EnterState(fsm);
            fsm.blackBoard.animancer.Play(fsm.blackBoard.attack);
        }

        public override void UpdateState(AIFSM fsm){
            base.UpdateState(fsm);
        }

        public override void ExitState(AIFSM fsm){
            base.ExitState(fsm);
        }
    }
}