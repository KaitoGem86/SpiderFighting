using EasyCharacterMovement;
using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController
{
    public class BaseCharacterAction<T> : ScriptableObject, ICharacterAction where T : MyCharacterController<T>
    {
        protected MyCharacterController<T> _playerController;
        [SerializeField] protected ActionEnum _actionEnum;
        //protected CharacterStateComponent<T> _stateContainer;
        //protected CharacterDisplayComponent<T> _displayContainer;
        //protected CharacterStatComponent<T> _statManager;
        [SerializeField] private bool _canChangeToItself = false;

        public virtual void Init(MyCharacterController<T> playerController, ActionEnum actionEnum)
        {
            _playerController = playerController;
            _actionEnum = actionEnum;
            // _stateContainer = _playerController.ResolveComponent<CharacterStateComponent<T>>(CharacterComponentEnum.State);
            // _displayContainer = _playerController.ResolveComponent<CharacterDisplayComponent<T>>(CharacterComponentEnum.Display);
            // _statManager = _playerController.ResolveComponent<CharacterStatComponent<T>>(CharacterComponentEnum.Stat);
        }

        public virtual void Enter(ActionEnum actionBefore)
        {

        }

        public virtual bool Exit(ActionEnum actionAfter)
        {
            return true;
        }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void OnCollisionEnter(Collision collision) { }

        public virtual void OnCollisionExit(Collision collision) { }

        public virtual void OnCollisionStay(Collision collision) { }

        public virtual void OnTriggerEnter(Collider other) { }

        public virtual void OnTriggerExit(Collider other) { }

        public virtual void OnTriggerStay(Collider other) { }
        public virtual void OnCollided(ref CollisionResult collisionResult){}

        public bool CanChangeToItself => _canChangeToItself;
        public ActionEnum ActionEnum => _actionEnum;
    }
}