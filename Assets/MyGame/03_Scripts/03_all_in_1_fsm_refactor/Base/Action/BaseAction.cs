using Extensions.SystemGame.MyCharacterController;

namespace Core.Test.Player{
    public class BaseAction : BaseCharacterAction<BlackBoard>{
        protected bool _isPlayer;
        public override void Init(MyCharacterController<BlackBoard> playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _isPlayer = _blackBoard.IsPlayer;
        }
    }
}