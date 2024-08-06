using Animancer;
using EasyCharacterMovement;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class InAirState<T> : LocalmotionState<T> where T : ITransition
    {
        public override void Update()
        {
            base.Update();
            GetInput();
        }

        public virtual void LateUpdate()
        {
            Move();
            Rotate();
        }

        protected override void Move()
        {
            Vector3 tmp = _moveDirection * _speed;
            Debug.DrawRay(_fsm.blackBoard.Character.transform.position, tmp, Color.red);
            _fsm.blackBoard.Character.AddForce(tmp);
        }

        protected override void GetInput()
        {
            base.GetInput();
            _moveDirection = Vector3.Project(_moveDirection, _fsm.blackBoard.PlayerDisplay.forward);
            // if (_moveDirection == Vector3.zero)
            // {
            //     _moveDirection = _fsm.blackBoard.CameraTransform.forward;
            //     _moveDirection.y = 0;
            // }
            // if (_rotateDirection == Vector3.zero)
            // {
            //     _rotateDirection = _fsm.blackBoard.CameraTransform.forward;
            //     _rotateDirection.y = 0;
            // }
        }

        public override void OnCollided(ref CollisionResult collision)
        {
            base.OnCollided(ref collision);
            var surfaceNormal = collision.surfaceNormal;
            var angle = Vector3.Angle(Vector3.up, surfaceNormal);
            if (angle < 45)
            {
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Landing);
            }
            else
            {
                // RaycastHit hit;
                // if (Physics.Raycast(_fsm.blackBoard.Character.transform.position, -surfaceNormal, out hit, 100))
                // {
                //     if (hit.distance < 0.8f)
                //     {
                //         _surfaceNormal = hit.normal;
                //         _fsm.blackBoard.RuntimeSurfaceNormal = _surfaceNormal;
                //         _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Climbing);
                //     }
                // }
            }
        }

        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            var surfaceNormal = collision.contacts[0].normal;
            var angle = Vector3.Angle(Vector3.up, surfaceNormal);
            if (angle < 45)
            {
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Landing);
            }
            else
            {
                // RaycastHit hit;
                // if (Physics.Raycast(_fsm.blackBoard.Character.transform.position, -surfaceNormal, out hit, 100))
                // {
                //     if (hit.distance < 0.8f)
                //     {
                //         _surfaceNormal = hit.normal;
                //         _fsm.blackBoard.RuntimeSurfaceNormal = _surfaceNormal;
                //         _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Climbing);
                //     }
                // }
            }
        }
    }
}