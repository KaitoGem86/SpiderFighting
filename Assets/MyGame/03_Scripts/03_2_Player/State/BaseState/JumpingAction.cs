using Animancer;
using Core.SystemGame;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(JumpingAction)), order = 0)]
    public class JumpingAction : InAirAction
    {
        [SerializeField] private ClipTransition _keepJumping;
        private bool _isJumping = false;
        private float _elapsedTime = 1f;
        private bool _isStartJumping = false;
        protected float _jumpVelocity = 0;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _isJumping = false;
            _jumpVelocity = 5f;
            _speed = 10;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            if (_isJumping) return;
            base.Enter(beforeAction);
            _speed = 5;
            _playerController.Jump();
            _isStartJumping = true;
            _elapsedTime = 0.3f;
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

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public void KeepJumping()
        {
            if (_keepJumping.Clip != null)
                _state = _displayContainer.PlayAnimation(_keepJumping);
            else
                FallingDown();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _isJumping = false;
            return base.Exit(actionAfter);
        }

        public void FallingDown()
        {
            _stateContainer.ChangeAction(ActionEnum.FallingDown);
        }

        public override void OnCollisionEnter(Collision collision)
        {
            if (_isStartJumping && _elapsedTime < 0)
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