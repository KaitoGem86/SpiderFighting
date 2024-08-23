using Animancer;
using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public enum InterpolateMode{
        None,
        ByGlobalSpeed,
        BySpeed,
        ByDirection,
    }

    public class BasePlayerState<T> : BaseState<T, PlayerBlackBoard>, IPlayerState where T : ITransition
    {
        [SerializeField] protected MovementMode _movementMode; 
        [SerializeField] protected InterpolateMode _interpolateMode;
        [SerializeField] protected float _speed;

        public override void EnterState()
        {
            base.EnterState();
            _fsm.blackBoard.Character.SetMovementMode(_movementMode);
            if(_movementMode == MovementMode.None)
            {
                var rigidbody = _fsm.blackBoard.Character.GetCharacterMovement().rigidbody;
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
                 rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                switch(_interpolateMode){
                    case InterpolateMode.None:
                        rigidbody.velocity = Vector3.zero;
                        break;
                    case InterpolateMode.ByGlobalSpeed:
                    case InterpolateMode.ByDirection:
                        rigidbody.velocity = _fsm.blackBoard.GlobalVelocity;
                        break;
                    case InterpolateMode.BySpeed:
                        rigidbody.velocity = _fsm.blackBoard.GlobalVelocity.normalized * _speed;
                        break;
                }
            }
            else{
                var rigidbody = _fsm.blackBoard.Character.GetCharacterMovement().rigidbody;
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX ;
                switch(_interpolateMode){
                    case InterpolateMode.None:
                        _fsm.blackBoard.Character.SetMovementDirection(Vector3.zero);
                        break;
                    case InterpolateMode.ByGlobalSpeed:
                        _fsm.blackBoard.Character.SetVelocity(_fsm.blackBoard.GlobalVelocity);
                        break;
                    case InterpolateMode.BySpeed:
                        _fsm.blackBoard.Character.SetVelocity(_fsm.blackBoard.GlobalVelocity.normalized * _speed);
                        break;
                    case InterpolateMode.ByDirection:
                        _fsm.blackBoard.Character.SetMovementDirection(_fsm.blackBoard.GlobalVelocity);
                        break;
                }
            }
        }

        public override void ExitState()
        {
            _fsm.blackBoard.GlobalVelocity = _fsm.blackBoard.GetVelocity;
            base.ExitState();
        }

        public virtual void OnCollided(ref CollisionResult collisionResult) { }
        public virtual void OnCollisionEnter(Collision collision) { }
 
        public void Swing(){
            if(_fsm.transform.position.y > 180) return;
            _fsm.ChangeAction(FSMState.Swing);
        }

        public void Jump(){
            _fsm.ChangeAction(FSMState.Jumping);
        }

        public virtual void Attack(){
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.StartAttack);
        }

        public virtual void Dodge(){
            _fsm.ChangeAction(FSMState.Dodge);
        }

        public virtual void UltimateAttackState(){
            _fsm.ChangeAction(FSMState.UltimateAttack);
        }

        public virtual void UseGadget(){
            _fsm.ChangeAction(FSMState.UseGadget);
        }

        public virtual void Zip(){
            if(_blackBoard.CameraFindZipPoint.zipPoint == Vector3.zero) return;
            _fsm.ChangeAction(FSMState.Zip);
        }
    }

    public class ClipTransitionPlayerState : BasePlayerState<ClipTransition> { }
    public class LinearMixerTransitionPlayerState : BasePlayerState<LinearMixerTransition> { }
}