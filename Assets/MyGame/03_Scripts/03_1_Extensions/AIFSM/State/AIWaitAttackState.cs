using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    [CreateAssetMenu(menuName = "AIFSM/AIWaitAttackState")]
    public class AIWaitAttackState : BaseState{
        public override void EnterState(AIFSM fsm){
            base.EnterState(fsm);
            fsm.blackBoard.animancer.Play(fsm.blackBoard.idle);
        }

        public override void UpdateState(AIFSM fsm){
            base.UpdateState(fsm);
        }

        public override void ExitState(AIFSM fsm){
            base.ExitState(fsm);
        }
    }
}