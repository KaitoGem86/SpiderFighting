using Animancer;
using Core.SystemGame;
using TMPro;
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
        private float _speedFromSwing = 0;
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
            var velocity = _playerController.GlobalVelocity;
            velocity.y = 0;
            _speed = velocity.magnitude;
            switch (beforeAction)
            {
                case ActionEnum.Zip:
                    _jumpVelocity = 15;
                    _speed = 20;
                    JumpFromZip();
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
                case ActionEnum.Swing:
                    _jumpVelocity = 5;
                    _playerController.SetVelocity(JumpVelocityFromSwing());
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
                case ActionEnum.Climbing:
                    _jumpVelocity = 5;
                    _isStartJumping = true;
                    JumpVelocityFromClimp();
                    _elapsedTime = 0.3f;
                    break;
                default:
                    _playerController.Jump();
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
            }
        }

        public override void Update()
        {
            base.Update();
            _elapsedTime -= Time.deltaTime;
            if (_beforeAction == ActionEnum.Climbing)
            {
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

        public override void ExitAction()
        {
            FallingDown();
        }


        public override void OnCollisionEnter(Collision collision)
        {
            if (_isStartJumping && _elapsedTime < 0)
                base.OnCollisionEnter(collision);
        }

        private Vector3 JumpVelocityFromSwing()
        {
            var velocity = _playerController.GlobalVelocity;
            velocity.y = 0;
            velocity = velocity.normalized;
            GetInput();
            var input = _moveDirection;
            _speedFromSwing = Mathf.Clamp(_speedFromSwing, 0, 40);
            if (Vector3.Angle(_playerController.GlobalVelocity, velocity) > 30)
            {
                Debug.Log("JumpingAction 1");
                var tmp = (velocity + input).normalized * _speedFromSwing + _playerController.GlobalVelocity + Vector3.up * _jumpVelocity;
                tmp.y = Mathf.Clamp(tmp.y, 20, 30);
                return tmp;
            }
            else
            {
                var tmp = _playerController.GlobalVelocity + Vector3.up * _jumpVelocity;
                tmp.y = Mathf.Clamp(tmp.y, 20, 30);
                return tmp;
            }
        }

        private void JumpVelocityFromClimp()
        {
            _playerController.SetVelocity(-_playerController.PlayerDisplay.forward * 20 + Vector3.up * _jumpVelocity);
        }

        private void JumpFromZip()
        {
           // _playerController.SetVelocity(-_playerController.PlayerDisplay.forward * 20 + Vector3.up * _jumpVelocity);
            _playerController.SetVelocity(Vector3.up *30);
        }
    }
}