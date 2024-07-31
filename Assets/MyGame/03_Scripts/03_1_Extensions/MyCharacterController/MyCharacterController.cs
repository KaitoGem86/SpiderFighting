using System.Collections.Generic;
using Core.GamePlay.Player;
using Core.Test.Player;
using EasyCharacterMovement;
using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController
{
    public class MyCharacterController<T> : MonoBehaviour where T : CharacterBlackBoard
    {
        [Header("============== CUSTOM COMPONENTS ==============")]
        [SerializeField] private List<BaseCharacterComponent<T>> _listComponents;
        [SerializeField] private T _blackBoard;
        private Dictionary<CharacterComponentEnum, BaseCharacterComponent<T>> _dictComponents;

        protected void Awake()
        {
            Init();
        }

        private void Init()
        {
            _dictComponents = new Dictionary<CharacterComponentEnum, BaseCharacterComponent<T>>();
            foreach (var item in _listComponents)
            {
                _dictComponents.Add(item.ComponentEnum, item);
                item.Init(this);
            }
        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        protected void OnCollisionEnter(Collision other)
        {
            foreach (var item in _dictComponents)
            {
                if (item.Value is ICharacterLoop)
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
            foreach (var item in _dictComponents)
            {
                if (item.Value is ICharacterLoop)
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
            foreach (var item in _dictComponents)
            {
                if (item.Value is ICharacterLoop)
                    item.Value.OnCollisionStay(other);
            }
        }

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        protected void OnTriggerEnter(Collider other)
        {
            foreach (var item in _dictComponents)
            {
                if (item.Value is ICharacterLoop)
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
            foreach (var item in _dictComponents)
            {
                if (item.Value is ICharacterLoop)
                    item.Value.OnTriggerStay(other);
            }
        }

        protected void OnCollided(ref CollisionResult collisionResult)
        {
            foreach (var item in _dictComponents)
            {
                if (item.Value is ICharacterLoop)
                    item.Value.OnCollided(ref collisionResult);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            foreach (var item in _dictComponents)
            {
                if (item.Value is ICharacterLoop)
                    item.Value.OnTriggerExit(other);
            }
        }


        public T1 ResolveComponent<T1>(CharacterComponentEnum component) where T1 : BaseCharacterComponent<T>
        {
            if (_dictComponents[component] is T1) return (T1)_dictComponents[component];
            else
            {
                throw new System.Exception("Invalid Component Type " + typeof(T).Name + " for " + component.ToString() + " Component");
            }
        }

        public T BlackBoard => _blackBoard;
    }
}