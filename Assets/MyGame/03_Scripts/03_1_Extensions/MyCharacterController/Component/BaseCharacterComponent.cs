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

    public class BaseCharacterComponent<T> : ScriptableObject where T : MyCharacterController<T>
    {
        protected T _controller;
        CharacterBlackBoard<T> _blackBoard;
        [SerializeField] private CharacterComponentEnum _componentEnum;

        public virtual void Init(CharacterBlackBoard<T> blackBoard)
        {
            _blackBoard = blackBoard;
            _controller = blackBoard.Controller;
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