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

        public override void Enter()
        {
            _speed = 5;
            var velocityVec3 = _playerController.CharacterMovement.velocity;
            var vel = velocityVec3.magnitude;
            velocityVec3.y = _playerController.CharacterMovement.rigidbody.velocity.y;
            var velocity = velocityVec3.magnitude;
            if(vel < 0.1f){
                _state = _displayContainer.PlayAnimation(_waitLanding.Clip, _waitLanding.FadeDuration);
                _state.Speed = _waitLanding.Speed;
                _state.Events = _waitLanding.Events;
                return;
            }
            if (velocity < _landingVelocityThreshold){
                _state = _displayContainer.PlayAnimation(_animationClip.Clip, _animationClip.FadeDuration);
                _state.Speed = _animationClip.Speed;
                _state.Events = _animationClip.Events;
            }
            else
            {
                _state = _displayContainer.PlayAnimation(_criticalLanding.Clip, _criticalLanding.FadeDuration);
                _state.Speed = _animationClip.Speed;
                _state.Events = _animationClip.Events;
            }
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
            MoveInAir();
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