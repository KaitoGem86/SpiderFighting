using AYellowpaper.SerializedCollections;
using EasyCharacterMovement;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _playerDisplay;
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private Transform _holdPivot;
        public Transform MeleeWeapon;
        private bool _isSprint = false;

        [SerializeField] private SerializedDictionary<PlayerComponentEnum, BasePlayerComponent> _dictPlayerComponents;
        private ActionEnum _beforeAction;

        private void Awake()
        {
            _characterMovement.detectCollisions = true;
            Init();
        }

        private void Init()
        {;
           // _dictPlayerComponents[PlayerComponentEnum.Display].Init(this);
            //_dictPlayerComponents[PlayerComponentEnum.State].Init(this);
           // _dictPlayerComponents[PlayerComponentEnum.Action].Init(this);
           // _dictPlayerComponents[PlayerComponentEnum.Stat].Init(this);
        }

        private void Update()
        {
            Debug.Log(InputManager.instance.move);
            if(Input.GetKeyDown(KeyCode.F)){
                ResolveComponent<PlayerStateComponent>(PlayerComponentEnum.State).ChangeAction(ActionEnum.HoldMelee);
            }
            if(Input.GetKeyDown(KeyCode.I)){
                ResolveComponent<PlayerStateComponent>(PlayerComponentEnum.State).ChangeAction(ActionEnum.InteractWithObject);
            }
        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        private void OnCollisionEnter(Collision other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if(item.Value is IPlayerLoop)
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
                if(item.Value is IPlayerLoop)
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
                if(item.Value is IPlayerLoop)
                    item.Value.OnCollisionStay(other);
            }
        }

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        private void OnTriggerEnter(Collider other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if(item.Value is IPlayerLoop)
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
                if(item.Value is IPlayerLoop)
                    item.Value.OnTriggerStay(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if(item.Value is IPlayerLoop)
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
        public Transform CameraTransform => _cameraTransform;
        public Transform HoldPivot => _holdPivot;
        public ActionEnum BeforeAction => _beforeAction;
        public bool IsCanMove { get; set; }
        public bool IsSprinting => _isSprint;
        public CharacterMovement CharacterMovement => _characterMovement;
    }
}