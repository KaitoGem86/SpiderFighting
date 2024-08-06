using Core.GamePlay.Player;
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
        public Vector3 GetVelocity{
            get {
                if(Character.GetMovementMode() == MovementMode.None)
                    return Character.GetCharacterMovement().rigidbody.velocity;
                else 
                    return Character.GetVelocity();
            }
        }
        public Transform HoldPivot;
        public Rigidbody SwingPivot;
        public PlayerModel PlayerModel;
    }
}