using EasyCharacterMovement;
using UnityEngine;

namespace Core.GamePlay.Player
{
    public class InAirAction : LocalmotionAction
    {
        protected Vector3 _initialVelocity;

        public override void Enter(ActionEnum beforeAction)
        {
            _initialVelocity = _playerController.GlobalVelocity;
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
            Rotate();
        }

        protected override void GetInput()
        {
            base.GetInput();
            // if (_moveDirection == Vector3.zero)
            // {
            //     _moveDirection = _playerController.CameraTransform.forward;
            //     _moveDirection.y = 0;
            // }
            // if (_rotateDirection == Vector3.zero)
            // {
            //     _rotateDirection = _playerController.CameraTransform.forward;
            //     _rotateDirection.y = 0;
            // }
        }

        protected override void Move()
        {
            Vector3 tmp = _moveDirection * _speed;
            tmp.y = _playerController.GetVelocity().y;
            if (_moveDirection.magnitude > 0.1f)
                _playerController.SetVelocity(tmp);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public override void OnCollided(ref CollisionResult collision)
        {
            base.OnCollided(ref collision);
            var surfaceNormal = collision.surfaceNormal;
            var angle = Vector3.Angle(Vector3.up, surfaceNormal);
            if (angle < 45)
            {
                _stateContainer.ChangeAction(ActionEnum.Landing);
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(_playerController.transform.position, -surfaceNormal, out hit, 100))
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

        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            var surfaceNormal = collision.contacts[0].normal;
            var angle = Vector3.Angle(Vector3.up, surfaceNormal);
            if (angle < 45)
            {
                _stateContainer.ChangeAction(ActionEnum.Landing);
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(_playerController.transform.position, -surfaceNormal, out hit, 100))
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