using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class PlayerBlackBoard : BlackBoard{
        public IPlayerState CurrentState;
        
        [Header("Movement")]
        public Transform CameraTransform;
        public Transform PlayerDisplay;
        public Character Character;
        public Vector3 RuntimeSurfaceNormal;
    }
}