using Extensions.SystemGame.MyCharacterController;

namespace Core.Test.Player{
    public class BaseAIAction : BaseCharacterAction<EnemyBlackBoard>{
        protected bool _isPlayer;
        public override void Init(MyCharacterController<EnemyBlackBoard> playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _isPlayer = _blackBoard.IsPlayer;
        }
    }
}