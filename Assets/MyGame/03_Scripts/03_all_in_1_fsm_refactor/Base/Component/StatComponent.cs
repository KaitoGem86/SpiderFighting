using Extensions.SystemGame.MyCharacterController;
using UnityEngine;

namespace Core.Test.Player{
    [CreateAssetMenu(menuName = "MyGame/FSM/Components/StatComponent")]
    public class StatComponent : CharacterStatComponent <BlackBoard>
    {
    }
}