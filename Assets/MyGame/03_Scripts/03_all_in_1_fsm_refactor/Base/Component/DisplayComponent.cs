using System.Collections.Generic;
using Extensions.SystemGame.MyCharacterController;
using UnityEngine;

namespace Core.Test.Player
{
    [CreateAssetMenu(menuName = "MyGame/Player/Components/PlayerDisplayComponent")]
    public class DisplayComponent : CharacterDisplayComponent<BlackBoard>
    {
        public override void Init(MyCharacterController<BlackBoard> controller)
        {
            base.Init(controller);
        }
    }
}