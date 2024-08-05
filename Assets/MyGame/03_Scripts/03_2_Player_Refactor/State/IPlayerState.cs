using EasyCharacterMovement;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public interface IPlayerState
    {
        void EnterState();
        void Update();
        void ExitState();
        public void OnCollided(ref CollisionResult collisionResult) ;
        public void OnCollisionEnter(Collision collision);
    }
}