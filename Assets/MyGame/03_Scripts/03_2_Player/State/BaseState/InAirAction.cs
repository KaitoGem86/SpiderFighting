using UnityEngine;

namespace Core.GamePlay.Player
{
    public class InAirAction : LocalmotionAction
    {
        public override void Enter()
        {
            _playerController.CharacterMovement.rigidbody.useGravity = true;
            _playerController.CharacterMovement.rigidbody.isKinematic = false;
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            base.Enter();
        }

        public override void LateUpdate()
        {
            GetInput();
            MoveInAir();
            if(_rotateDirection != Vector3.zero)
                Rotate();
        }

        protected override void MoveInAir()
        {
            Vector3 tmp = _moveDirection * _speed;
            tmp.y = _playerController.CharacterMovement.velocity.y;
            _playerController.CharacterMovement.Move(tmp);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _playerController.CharacterMovement.rigidbody.useGravity = false;
            _playerController.CharacterMovement.rigidbody.isKinematic = true;
            return base.Exit(actionAfter);
        }

        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            var surfaceNormal = collision.contacts[0].normal;
            var angle = Vector3.Angle(Vector3.up, surfaceNormal);
            if (angle < 45)
            {
                Debug.Log("Landing " + collision.gameObject.name);
                _stateContainer.ChangeAction(ActionEnum.Landing);
            }
            else
            {
                //EndStateToClimb();
                _stateContainer.SurfaceNormal = surfaceNormal;
                _stateContainer.ChangeAction(ActionEnum.Climbing);
            }
        }

        protected virtual void EndStateToClimb()
        {
        }
    }
}