using System.Collections;
using Animancer;
using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using SFRemastered.InputSystem;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class BasePlayerState<T> : BaseState<T, PlayerBlackBoard>, IPlayerState where T : ITransition
    {
        [SerializeField] protected MovementMode _movementMode; 
        [SerializeField] protected bool _isInterpolating;
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
                rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                rigidbody.velocity = _isInterpolating ? _fsm.blackBoard.GlobalVelocity : _fsm.blackBoard.GlobalVelocity.normalized * _speed;
            }
            else{
                var rigidbody = _fsm.blackBoard.Character.GetCharacterMovement().rigidbody;
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                _fsm.blackBoard.Character.SetVelocity(_isInterpolating ? _fsm.blackBoard.GlobalVelocity : _fsm.blackBoard.GlobalVelocity.normalized * _speed);
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
    }

    public class ClipTransitionPlayerState : BasePlayerState<ClipTransition> { }
    public class LinearMixerTransitionPlayerState : BasePlayerState<LinearMixerTransition> { }
}