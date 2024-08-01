using UnityEngine;
namespace Extensions.SystemGame.AIFSM{
    public class AIAttackState : ClipTransitionState{
        public override void EnterState(){
            base.EnterState();
            _fsm.blackBoard.animancer.Play(_fsm.blackBoard.attack);
        }

        public override void Update(){
            base.Update();
        }

        public override void ExitState(){
            base.ExitState();
        }
    }
}