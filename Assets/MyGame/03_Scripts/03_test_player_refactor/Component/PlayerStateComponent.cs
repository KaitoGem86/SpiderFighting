using Extensions.SystemGame.MyCharacterController;
using UnityEngine;

namespace Core.Test.Player{
    [CreateAssetMenu(menuName = "MyGame/Player/Components/PlayerStateComponent")]
    public class PlayerStateComponent : CharacterStateComponent<PlayerBlackBoard>
    {
        public override void Init(MyCharacterController<PlayerBlackBoard> controller)
        {
            base.Init(controller);
        }
    }
}