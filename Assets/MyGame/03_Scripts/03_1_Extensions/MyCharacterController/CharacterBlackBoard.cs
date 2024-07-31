using System.Collections.Generic;
using Core.Test.Player;
using EasyCharacterMovement;
using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController{
    public class CharacterBlackBoard : MonoBehaviour 
    {
        public List<CharacterModel> characterModels;
        public Character character;
        [HideInInspector] public CharacterModel CurrentModel;
    }
}