using System.Collections.Generic;
using Extensions.SystemGame.MyCharacterController;
using UnityEngine;

namespace Core.Test.Player
{
    [CreateAssetMenu(menuName = "MyGame/Player/Components/PlayerDisplayComponent")]
    public class PlayerDisplayComponent : CharacterDisplayComponent<PlayerBlackBoard>
    {
        public override void Init(MyCharacterController<PlayerBlackBoard> controller)
        {
            base.Init(controller);
        }
    }
}