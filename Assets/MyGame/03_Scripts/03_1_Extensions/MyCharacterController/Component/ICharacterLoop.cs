using EasyCharacterMovement;
using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController{
    public interface ICharacterLoop{
        public void Update();
        public void LateUpdate();
        public void FixedUpdate();

        public void OnCollisionEnter(Collision collision);
        public void OnCollisionExit(Collision collision);
        public void OnCollisionStay(Collision collision);
        public void OnTriggerEnter(Collider other);
        public void OnTriggerExit(Collider other);
        public void OnTriggerStay(Collider other);
        public void OnCollided(ref CollisionResult collisionResult);
    }
}