using System.Security.Cryptography;
using Animancer;
using Core.GamePlay.Player;
using Core.GamePlay.Support;
using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class PlayerBlackBoard : BlackBoard{
        public IPlayerState CurrentState;
        [Header("Player Data")]
        public PlayerData PlayerData;

        [Header("Skin")]
        public PlayerModel[] PlayerModels;
        public PlayerModel CurrentPlayerModel;
        public AnimancerComponent Animancer;
        public AnimancerState CurrentAnimancerState;
        
        [Header("Movement")]
        public Transform CameraTransform;
        public Transform PlayerDisplay => CurrentPlayerModel.PlayerDisplay;
        public Character Character;
        public Vector3 RuntimeSurfaceNormal;
        public Transform CheckWallPivot;
        public BoolEvent OnReachMaxSpeed;
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
        public Vector3 GlobalVelocity;
        public Rigidbody rig;

        [Header("Combat")]
        public FindEnemyToAttack FindEnemyToAttack;
        public Transform HealingBotPivot;
        public GadgetsController GadgetsController;
    }
}