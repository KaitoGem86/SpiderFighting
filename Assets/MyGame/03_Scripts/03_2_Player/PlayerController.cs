using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Support;
using EasyCharacterMovement;
using MyTools.Event;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerController : Character
    {
        [SerializeField] private Transform _cameraTransformObject;
        [SerializeField] private CharacterMovement _characterMovementObject;
        [SerializeField] private Transform _holdPivot;
        [SerializeField] private BoolEvent _onReachMaxSpeed;
        public LineRenderer LeftLine;
        public LineRenderer RightLine;
        public PlayerModel CurrentPlayerModel;

        [SerializeField] private SerializedDictionary<PlayerComponentEnum, BasePlayerComponent> _dictPlayerComponents;

        protected override void Awake()
        {
            base.Awake();   
            Init();
        }

        private void Init()
        {
            _dictPlayerComponents[PlayerComponentEnum.Display].Init(this);
            _dictPlayerComponents[PlayerComponentEnum.State].Init(this);
        }

        protected override void Update(){
            if(GetVelocity().magnitude > 60){
                _onReachMaxSpeed.Raise(true);
            }
            else{
                _onReachMaxSpeed.Raise(false);
            }
        }
        public override Vector3 GetVelocity()
        {
            if(GetMovementMode() == MovementMode.None) return CharacterMovement.rigidbody.velocity;
            return base.GetVelocity();
        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        protected void OnCollisionEnter(Collision other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is IPlayerLoop)
                    item.Value.OnCollisionEnter(other);
            }
        }

        /// <summary>
        /// OnCollisionExit is called when this collider/rigidbody has
        /// stopped touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        private void OnCollisionExit(Collision other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is IPlayerLoop)
                    item.Value.OnCollisionExit(other);
            }
        }

        /// <summary>
        /// OnCollisionStay is called once per frame for every collider/rigidbody
        /// that is touching rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        private void OnCollisionStay(Collision other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is IPlayerLoop)
                    item.Value.OnCollisionStay(other);
            }
        }

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        protected override void OnTriggerEnter(Collider other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is IPlayerLoop)
                    item.Value.OnTriggerEnter(other);
            }
        }

        /// <summary>
        /// OnTriggerStay is called once per frame for every Collider other
        /// that is touching the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        private void OnTriggerStay(Collider other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is IPlayerLoop)
                    item.Value.OnTriggerStay(other);
            }
        }

        protected override void OnCollided(ref CollisionResult collisionResult)
        {
            base.OnCollided(ref collisionResult);
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is IPlayerLoop)
                    item.Value.OnCollided(ref collisionResult);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is IPlayerLoop)
                    item.Value.OnTriggerExit(other);
            }
        }


        public T ResolveComponent<T>(PlayerComponentEnum component) where T : BasePlayerComponent
        {
            if (_dictPlayerComponents[component] is T) return (T)_dictPlayerComponents[component];
            else
            {
                throw new System.Exception("Invalid Component Type " + typeof(T).Name + " for " + component.ToString() + " Component");
            }
        }

        public Transform PlayerDisplay => CurrentPlayerModel.PlayerDisplay;
        public Transform CameraTransform => _cameraTransformObject;
        public Transform HoldPivot => _holdPivot;
        public CharacterMovement CharacterMovement => _characterMovementObject;
        public DisplayZipPoint DisplayZipPoint;
        public Rigidbody swingPivot;
        public Vector3 GlobalVelocity;
        public Transform leftHand => CurrentPlayerModel.leftHand;
        public Transform rightHand => CurrentPlayerModel.rightHand;
        public Transform CheckWallPivot => CurrentPlayerModel.checkWallPivot;
    }
}