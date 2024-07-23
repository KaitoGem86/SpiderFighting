using EasyCharacterMovement;
using UnityEngine;

namespace Core.GamePlay.Player
{
    public class InAirAction : LocalmotionAction
    {
        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
        }

        public override void Update()
        {
            base.Update();
            GetInput();
        }

        public override void LateUpdate()
        {
            Move();
            if (_rotateDirection != Vector3.zero)
                Rotate();
        }

        protected override void Move()
        {
            Vector3 tmp = _moveDirection * _speed;
            tmp.y = _playerController.GetVelocity().y;
            _playerController.SetVelocity(tmp);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public override void OnCollided(ref CollisionResult collision)
        {
            base.OnCollided( ref collision);
            var surfaceNormal = collision.surfaceNormal;
            var angle = Vector3.Angle(Vector3.up, surfaceNormal);
            if (angle < 45)
            {
                _stateContainer.ChangeAction(ActionEnum.Landing);
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(_playerController.transform.position, _playerController.PlayerDisplay.forward, out hit, 100))
                {
                    if (hit.distance < 0.8f)
                    {
                        _surfaceNormal = hit.normal;
                        _stateContainer.SurfaceNormal = _surfaceNormal;
                        EndStateToClimb();
                        _stateContainer.ChangeAction(ActionEnum.Climbing);
                    }
                }
            }
        }

        protected virtual void EndStateToClimb()
        {
        }
    }
}