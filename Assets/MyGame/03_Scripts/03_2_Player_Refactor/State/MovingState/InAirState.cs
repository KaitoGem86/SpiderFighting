using Animancer;
using EasyCharacterMovement;
using TMPro;
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

        public virtual void FixedUpdate()
        {
            Move();
            //Rotate();
        }

        protected override void Rotate()
        {
            // var forward = _fsm.transform.forward;
            // forward.y = 0;
            // Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);
            // _fsm.transform.rotation = Quaternion.Lerp(_fsm.transform.rotation, rotation, 0.2f * 10 * Time.fixedDeltaTime);
            // var vel = _fsm.blackBoard.GetVelocity;
            // vel.y = 0;
            // _fsm.blackBoard.Character.RotateTowardsWithSlerp(vel, false);
            var rotation = _fsm.transform.rotation;
            rotation.x = 0;
            rotation.z = 0;
            _fsm.transform.rotation = rotation;
        }

        protected override void GetInput()
        {
            base.GetInput();
        }

        public override void OnCollided(ref CollisionResult collision)
        {
            base.OnCollided(ref collision);
            var surfaceNormal = collision.surfaceNormal;
            var angle = Vector3.Angle(Vector3.up, surfaceNormal);
            if (angle < 45)
            {
                if ((_blackBoard.GroundLayer.value & (1 << collision.collider.gameObject.layer)) == 0) return;
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
                if((_blackBoard.ClimbLayer.value & (1 << collision.collider.gameObject.layer)) == 0) return;
                _surfaceNormal = surfaceNormal;
                _fsm.blackBoard.RuntimeSurfaceNormal = _surfaceNormal;
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Climbing);
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
                RaycastHit hit;
                if (Physics.Raycast(_fsm.blackBoard.Character.transform.position, -surfaceNormal, out hit, 100))
                {
                    if (hit.distance < 0.8f)
                    {
                        _surfaceNormal = hit.normal;
                        _fsm.blackBoard.RuntimeSurfaceNormal = _surfaceNormal;
                        _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Climbing);
                    }
                }
            }
        }

        public override void Zip()
        {
            if(_blackBoard.CameraFindZipPoint.zipPoint != Vector3.zero)
                base.Zip();
            else{
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.AirZip);
            }
        }
    }
}