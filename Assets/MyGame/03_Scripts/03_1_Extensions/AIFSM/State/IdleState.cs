using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    [CreateAssetMenu(menuName = "AIFSM/IdleState")]
    public class IdleState : BaseState
    {
        public override void EnterState(AIFSM fsm)
        {
            Debug.Log("IdleState");
            base.EnterState(fsm);
        }

        public override void UpdateState(AIFSM fsm)
        {
            base.UpdateState(fsm);
        }

        public override void ExitState(AIFSM fsm)
        {
            base.ExitState(fsm);
        }
    }
}