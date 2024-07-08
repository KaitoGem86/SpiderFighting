using Animancer;
using Core.SystemGame;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(LandingAction), menuName = ("PlayerState/" + nameof(LandingAction)), order = 0)]
    public class LandingAction : LocalmotionAction
    {
        [SerializeField] private ClipTransition _criticalLanding;
        [SerializeField] private float _landingVelocityThreshold = 5;

        public override void Enter()
        {
            if (_stateContainer.VerticalVelocityValue > _landingVelocityThreshold)
            {
                _state = _displayContainer.PlayAnimation(_criticalLanding.Clip, _criticalLanding.FadeDuration, PlayerTypeAnimMask.Base);
                _state.Speed = _animationClip.Speed;
                _state.Events = _animationClip.Events;
                _stateContainer.VerticalVelocityValue = 0;
                _speed = 5;
                return;
            }
            base.Enter();
            _stateContainer.VerticalVelocityValue = 0;
            _speed = 5;
        }

        public void CompleteLanding()
        {
            //if)
            if (InputSystem.Instance.IsJump)
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            if (InputSystem.Instance.InputJoyStick.Direction.magnitude > 0.1f)
            {
                if (InputSystem.Instance.IsSprint)
                {
                    _stateContainer.ChangeAction(ActionEnum.Sprinting);
                    return;
                }
                else
                {
                    _stateContainer.ChangeAction(ActionEnum.Moving);
                    return;
                }
            }
            else
            {
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
        }

        public override void LateUpdate()
        {
            GetInput();

            var rayCheckGround = RaycastCheckGround();
            if (!rayCheckGround.Item1 && rayCheckGround.Item2 > 3)
            {
                _stateContainer.ChangeAction(ActionEnum.FallingDown);
                return;
            }
            MoveInAir();
            Rotate();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }
    }
}