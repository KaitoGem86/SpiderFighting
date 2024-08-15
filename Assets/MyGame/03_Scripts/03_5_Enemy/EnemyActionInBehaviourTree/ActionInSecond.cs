using Extensions.SystemGame.AIFSM;
using NodeCanvas.Framework;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class ActionInSecond : ActionTask{
        public FSMState fsmType;
        public string actionName;
        public BBParameter<BaseEnemyBlackBoard> blackBoard;
        public float timer;

        protected override string info => actionName;

        protected override void OnExecute()
        {
            var fsm = blackBoard.value.fsm;
            fsm.ChangeAction(fsmType);
        }

        protected override void OnUpdate()
        {
            if(elapsedTime > timer){
                EndAction(true);
            }
        }
    }
}