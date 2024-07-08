using Animancer;
using Core.SystemGame;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(JumpingAction)), order = 0)]
    public class JumpingAction : LocalmotionAction
    {
        private bool _isJumping = false;
        private bool _isStartedJumping = false;
        private float _jumpVelocity = 0;
        private float _jumpLength = 0;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _isJumping = false;
            _jumpVelocity = 10;
        }

        public override void Enter()
        {
            if (_isJumping) return;
            base.Enter();
            _isStartedJumping = false;
            _jumpLength = InputSystem.Instance.InputJoyStick.Direction.magnitude;
            _speed = 5;
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            GetInput();
            MoveInAir();
        }

        public void Jump()
        {
            _isStartedJumping = true;
            _isJumping = true;
            _stateContainer.VerticalVelocityValue = -_jumpVelocity;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            if (actionAfter == ActionEnum.Sprinting) return false;
            _isJumping = false;
            return base.Exit(actionAfter);
        }

        public void FallingDown()
        {
            _stateContainer.ChangeAction(ActionEnum.FallingDown);
        }

        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
        }

        public override void OnCollisionStay(Collision collision)
        {
        }

        public override void OnCollisionExit(Collision collision)
        {
        }
    }
}