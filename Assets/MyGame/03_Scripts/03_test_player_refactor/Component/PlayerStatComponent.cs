using Extensions.SystemGame.MyCharacterController;
using UnityEngine;

namespace Core.Test.Player{
    [CreateAssetMenu(menuName = "MyGame/Player/Components/PlayerStatComponent")]
    public class PlayerStatComponent : CharacterStatComponent <PlayerBlackBoard>
    {
    }
}