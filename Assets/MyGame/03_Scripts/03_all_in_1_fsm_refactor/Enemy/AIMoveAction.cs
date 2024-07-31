using Extensions.SystemGame.MyCharacterController;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Test.Player{
    [CreateAssetMenu(menuName = "MyGame/FSM/Actions/AIMoveAction")]
    public class AIMoveAction : BaseAIAction
    {
        private NavMeshAgent _navMeshAgent;
        private float _elapsetime;

        public override void Init(MyCharacterController<EnemyBlackBoard> playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _navMeshAgent = _blackBoard.navMeshAgent;
        }

        public override void Enter(ActionEnum actionBefore)
        {
            base.Enter(actionBefore);
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_blackBoard.target.position);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _navMeshAgent.isStopped = true;
            return base.Exit(actionAfter);
        }

        public override void Update()
        {
            base.Update();
            _elapsetime -= Time.deltaTime;
            if (_elapsetime <= 0)
            {
                _elapsetime = 0.5f;
                _navMeshAgent.SetDestination(_blackBoard.target.position);
            }
        }


    }
}