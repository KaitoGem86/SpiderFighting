using Extensions.SystemGame.MyCharacterController;
using UnityEngine;

namespace Core.Test.Player{
    [CreateAssetMenu(menuName = "MyGame/FSM/Components/StateComponent")]
    public class StateComponent : CharacterStateComponent<BlackBoard>
    {
        public override void Init(MyCharacterController<BlackBoard> controller)
        {
            base.Init(controller);
        }
    }
}