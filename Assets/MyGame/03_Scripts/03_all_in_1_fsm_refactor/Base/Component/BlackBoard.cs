using Extensions.SystemGame.MyCharacterController;
using UnityEngine;

namespace Core.Test.Player{
    public class BlackBoard : CharacterBlackBoard
    {
        public bool IsPlayer;
        public Transform Display => CurrentModel.transform;
    }
}