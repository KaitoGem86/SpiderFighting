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

    public class BaseCharacterComponent<T1, T2> : ScriptableObject where T1 : MyCharacterController<T1> where T2 : CharacterBlackBoard<T1>
    {
        protected T1 _controller;
        T2 _blackBoard;
        [SerializeField] private CharacterComponentEnum _componentEnum;

        public virtual void Init(T2 blackBoard)
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