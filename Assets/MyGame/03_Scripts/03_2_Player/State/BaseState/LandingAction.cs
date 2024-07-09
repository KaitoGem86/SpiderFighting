using Animancer;
using Core.SystemGame;
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
            var velocity = _playerController.CharacterMovement.velocity.magnitude;
            if(velocity < 0.1f){
                _state = _displayContainer.PlayAnimation(_waitLanding.Clip, _waitLanding.FadeDuration);
                _state.Speed = _waitLanding.Speed;
                _state.Events = _waitLanding.Events;
            }
            else if (velocity < _landingVelocityThreshold){
                _state = _displayContainer.PlayAnimation(_animationClip.Clip, _animationClip.FadeDuration);
                _state.Speed = _animationClip.Speed;
                _state.Events = _animationClip.Events;
            }
            else if (_playerController.CharacterMovement.velocity.magnitude > _landingVelocityThreshold)
            {
                _state = _displayContainer.PlayAnimation(_criticalLanding.Clip, _criticalLanding.FadeDuration);
                _state.Speed = _animationClip.Speed;
                _state.Events = _animationClip.Events;
            }
            _speed = 5;
        }

        public void CompleteLanding()
        {
            if(_playerController.CharacterMovement.velocity.magnitude < 0.1f){
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