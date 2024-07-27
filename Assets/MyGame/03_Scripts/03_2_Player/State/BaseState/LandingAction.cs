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
        [SerializeField] private float _landingVelocityThreshold = 4;

        public override void Enter(ActionEnum before)
        {
            _speed = _playerController.GlobalVelocity.magnitude;
            base.Enter(before);
        }

        public override void KeepAction()
        {
            base.KeepAction();
            var velocity = _playerController.GlobalVelocity.magnitude;
            GetInput();
            Debug.Log(_moveDirection);
            if (_moveDirection.magnitude < 0.1f)
            {
                _currentTransition.keepAnimation.State.Parameter = 0;
            }
            else if (velocity < _landingVelocityThreshold)
            {
                _currentTransition.keepAnimation.State.Parameter = 1;
            }
            else
            {
                _currentTransition.keepAnimation.State.Parameter = 2;
            }
        }

        public override void ExitAction()
        {
            CompleteLanding();
        }

        public void CompleteLanding()
        {
            var velocity = _playerController.CharacterMovement.velocity;
            velocity.y = 0;
            if (velocity.magnitude < 0.1f)
            {
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
            return base.Exit(actionAfter);
        }
    }
}