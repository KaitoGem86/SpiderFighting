using System.Security.Cryptography;
using Animancer;
using Core.GamePlay.Player;
using Core.GamePlay.Support;
using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class PlayerBlackBoard : BlackBoard{
        public IPlayerState CurrentState;

        [Header("Skin")]
        public PlayerModel[] PlayerModels;
        public PlayerModel CurrentPlayerModel;
        public AnimancerComponent Animancer;
        public AnimancerState CurrentAnimancerState;
        
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
        public Vector3 GlobalVelocity;

        [Header("Combat")]
        public FindEnemyToAttack FindEnemyToAttack;
        public Transform HealingBotPivot;
        public GadgetsController GadgetsController;
    }
}