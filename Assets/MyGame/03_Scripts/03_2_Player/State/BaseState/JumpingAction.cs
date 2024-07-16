using Animancer;
using Core.SystemGame;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(JumpingAction)), order = 0)]
    public class JumpingAction : InAirAction
    {
        private bool _isJumping = false;
        private float _elapsedTime = 1f;
        private bool _isStartJumping = false;
        protected float _jumpVelocity = 0;
        private ActionEnum _beforeAction;   
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _isJumping = false;
            _jumpVelocity = 10;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            if (_isJumping) return;
            base.Enter(beforeAction);
            _beforeAction = beforeAction;
            _playerController.gravity = Vector3.down * 9.8f;
            switch (beforeAction)
            {
                case ActionEnum.Zip:
                    _speed = 15;
                    _playerController.Jump();
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
                case ActionEnum.Swing:
                    _speed = 15;
                    _jumpVelocity = 20;
                    _playerController.SetVelocity(JumpDirection());
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
                case ActionEnum.Climbing:
                    _speed = 15;
                    _jumpVelocity = 5;
                    _playerController.CharacterMovement.rigidbody.velocity = -_playerController.PlayerDisplay.transform.forward * 5  + Vector3.up * 20;
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
                default:
                    _speed = 10;
                    _playerController.Jump();
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
            }
        }

        protected virtual Vector3 JumpDirection()
        {
            return _playerController.PlayerDisplay.forward * _playerController.GlobalVelocity.magnitude + Vector3.up * _jumpVelocity;
        }

        public override void Update()
        {
            base.Update();
            _elapsedTime -= Time.deltaTime;
            if(_beforeAction == ActionEnum.Climbing){
                _moveDirection = Vector3.zero;
                _rotateDirection = Vector3.zero;
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _isJumping = false;
            _playerController.StopJumping();
            return base.Exit(actionAfter);
        }

        private void FallingDown()
        {
            if (_beforeAction != ActionEnum.Swing)
                _stateContainer.ChangeAction(ActionEnum.FallingDown);
            else
                _stateContainer.ChangeAction(ActionEnum.Dive);
        }

        protected override void ExitAction()
        {
            FallingDown();
        }

        public override void OnCollisionEnter(Collision collision)
        {
            if (_isStartJumping && _elapsedTime < 0)
                base.OnCollisionEnter(collision);
        }
    }
}