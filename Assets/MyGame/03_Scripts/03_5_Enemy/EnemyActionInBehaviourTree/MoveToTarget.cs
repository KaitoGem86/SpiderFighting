using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Core.GamePlay.Enemy
{
    public class MoveToTarget : ActionTask
    {
        public float speed;
        public BBParameter<float> distance;
        public BBParameter<EnemyBlackBoard> blackBoard;
        public BBParameter<Vector3> targetPosition;
        public string targetName;

    
        protected override string info => "Move to " + targetName;

        protected override void OnExecute()
        {
            blackBoard.value.targetPosition = targetPosition.value;
            blackBoard.value.controller.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Moving);
        }

        protected override void OnUpdate()
        {
            blackBoard.value.targetPosition = targetPosition.value;
            var distance = Vector3.Distance(blackBoard.value.controller.transform.position, targetPosition.value);
            if (distance < this.distance.value)
            {
                EndAction(true);
            }
        }
    }
}