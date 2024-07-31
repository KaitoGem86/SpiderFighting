using System.Collections.Generic;
using Core.GamePlay.Player;
using EasyCharacterMovement;
using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController
{
    public class MyCharacterController<T> : Character where T : MyCharacterController<T>
    {
        [SerializeField] private List<BaseCharacterComponent<T>> _listPlayerComponents;
        [SerializeField] private CharacterBlackBoard<T> _blackBoard;
        private Dictionary<CharacterComponentEnum, BaseCharacterComponent<T>> _dictPlayerComponents;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            _dictPlayerComponents = new Dictionary<CharacterComponentEnum, BaseCharacterComponent<T>>();
            foreach (var item in _listPlayerComponents)
            {
                _dictPlayerComponents.Add(item.ComponentEnum, item);
                item.Init(_blackBoard);
            }
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
            foreach (var item in _dictPlayerComponents)
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
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is ICharacterLoop)
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
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is ICharacterLoop)
                    item.Value.OnTriggerStay(other);
            }
        }

        protected override void OnCollided(ref CollisionResult collisionResult)
        {
            base.OnCollided(ref collisionResult);
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is ICharacterLoop)
                    item.Value.OnCollided(ref collisionResult);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            foreach (var item in _dictPlayerComponents)
            {
                if (item.Value is ICharacterLoop)
                    item.Value.OnTriggerExit(other);
            }
        }


        public T1 ResolveComponent<T1>(CharacterComponentEnum component) where T1 : BaseCharacterComponent<T>
        {
            if (_dictPlayerComponents[component] is T1) return (T1)_dictPlayerComponents[component];
            else
            {
                throw new System.Exception("Invalid Component Type " + typeof(T).Name + " for " + component.ToString() + " Component");
            }
        }
    }
}