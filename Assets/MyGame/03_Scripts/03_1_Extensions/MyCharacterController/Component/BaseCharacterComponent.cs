using System.ComponentModel.Design;
using EasyCharacterMovement;
using UnityEngine;

namespace Extentions.SystemGame.MyCharacterController
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

    public class BaseCharacterComponent : MonoBehaviour
    {
        protected MyCharacterController _controller;

        public virtual void Init(MyCharacterController playerController)
        {
            _controller = playerController;
        }

        // public virtual void Update()
        // {
        // }

        // public virtual void LateUpdate()
        // {
        // }

        // public virtual void FixedUpdate()
        // {
        // }

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
    }
}