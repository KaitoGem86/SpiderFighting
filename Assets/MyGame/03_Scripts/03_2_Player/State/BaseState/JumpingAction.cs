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
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _isJumping = false;
            _jumpVelocity = 100f;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            if (_isJumping) return;
            base.Enter(beforeAction);
            switch (beforeAction)
            {
                case ActionEnum.Zip :
                    Debug.Log("Zip");
                    _speed = 10;
                    _playerController.Jump();
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
                default:
                    _speed = 5;
                    _playerController.Jump();
                    _isStartJumping = true;
                    _elapsedTime = 0.3f;
                    break;
            }

        }

        protected virtual Vector3 JumpDirection()
        {
            return Vector3.up * _jumpVelocity;
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stateContainer.ChangeAction(ActionEnum.Swing);
            }
            _elapsedTime -= Time.deltaTime;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _isJumping = false;
            _playerController.StopJumping();
            return base.Exit(actionAfter);
        }

        private void FallingDown()
        {
            _stateContainer.ChangeAction(ActionEnum.FallingDown);
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