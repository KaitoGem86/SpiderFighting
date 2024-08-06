using EasyCharacterMovement;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class PlayerCharacterController : Character{
        [SerializeField] private PlayerController _playerController;

        protected override void OnCollided(ref CollisionResult collisionResult)
        {
            base.OnCollided(ref collisionResult);
            _playerController.OnCollided(ref collisionResult);
        }
    }
}