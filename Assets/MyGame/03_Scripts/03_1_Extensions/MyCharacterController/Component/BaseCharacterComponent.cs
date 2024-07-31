using System.ComponentModel.Design;
using EasyCharacterMovement;
using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController
{
    public enum CharacterComponentEnum
    {
        None,
        State,
        Display,
        Inventory,
        Action,
        Stat,
    }

    public class BaseCharacterComponent<T1> : ScriptableObject where T1 : CharacterBlackBoard
    {
        protected MyCharacterController<T1> _controller;
        protected T1 _blackBoard;
        [SerializeField] private CharacterComponentEnum _componentEnum;

        public virtual void Init(MyCharacterController<T1> controller)
        {
            _controller = controller;
            _blackBoard = controller.BlackBoard;
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
        }

        public virtual void OnCollisionExit(Collision collision)
        {
        }

        public virtual void OnCollisionStay(Collision collision)
        {
        }

        public virtual void OnTriggerEnter(Collider other)
        {
        }

        public virtual void OnTriggerExit(Collider other)
        {
        }

        public virtual void OnTriggerStay(Collider other)
        {
        }

        public virtual void OnCollided(ref CollisionResult collisionResult)
        {
        }

        public CharacterComponentEnum ComponentEnum => _componentEnum;
    }
}