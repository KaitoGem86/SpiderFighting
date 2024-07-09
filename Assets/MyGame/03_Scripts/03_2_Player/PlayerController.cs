using AYellowpaper.SerializedCollections;
using EasyCharacterMovement;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerController : Character
    {
        [SerializeField] private Transform _cameraTransformObject;
        [SerializeField] private Transform _playerDisplay;
        [SerializeField] private CharacterMovement _characterMovementObject;
        [SerializeField] private Transform _holdPivot;
        public LineRenderer Line;
        public Transform MeleeWeapon;

        [SerializeField] private SerializedDictionary<PlayerComponentEnum, BasePlayerComponent> _dictPlayerComponents;
        private ActionEnum _beforeAction;

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

        public Transform PlayerDisplay => _playerDisplay;
        public Transform CameraTransform => _cameraTransformObject;
        public Transform HoldPivot => _holdPivot;
        public ActionEnum BeforeAction => _beforeAction;
        public bool IsCanMove { get; set; }
        public CharacterMovement CharacterMovement => _characterMovementObject;
    }
}