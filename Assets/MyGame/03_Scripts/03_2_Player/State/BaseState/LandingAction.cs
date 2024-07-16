using Animancer;
using Core.SystemGame;
using DG.Tweening.Plugins.Options;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(LandingAction), menuName = ("PlayerState/" + nameof(LandingAction)), order = 0)]
    public class LandingAction : LocalmotionAction
    {
        [SerializeField] private ClipTransition _criticalLanding;
        [SerializeField] private ClipTransition _waitLanding;
        [SerializeField] private float _landingVelocityThreshold = 5;

        public override void Enter(ActionEnum before)
        {
            _speed = 5;
            var velocityVec3 = _playerController.CharacterMovement.velocity;
            var vel = velocityVec3.magnitude;
            velocityVec3.y = _playerController.CharacterMovement.rigidbody.velocity.y;
            var velocity = velocityVec3.magnitude;
            if(vel < 0.1f){
                _state = _displayContainer.PlayAnimation(_waitLanding);
                return;
            }
            if (velocity < _landingVelocityThreshold){
                _state = _displayContainer.PlayAnimation(_animationClip);
            }
            else
            {
                _state = _displayContainer.PlayAnimation(_criticalLanding);
            }
            _playerController.CharacterMovement.rigidbody.velocity = Vector3.zero;
            _playerController.CharacterMovement.velocity = Vector3.zero;
            _playerController.SetVelocity(Vector3.zero);
        }

        public void CompleteLanding()
        {
            var velocity = _playerController.CharacterMovement.velocity;
            velocity.y = 0;
            if(velocity.magnitude < 0.1f){
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
            _stateContainer.ChangeAction(ActionEnum.Moving);
            return;
        }

        public override void LateUpdate()
        {
            GetInput();
            Move();
            Rotate();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _playerController.CharacterMovement.rigidbody.useGravity = false;
            _playerController.CharacterMovement.rigidbody.isKinematic = true;
            return base.Exit(actionAfter);
        }
    }
}