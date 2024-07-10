using Animancer;
using Core.SystemGame;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(JumpingAction)), order = 0)]
    public class JumpingAction : LocalmotionAction
    {
        [SerializeField] private ClipTransition _keepJumping;
        private bool _isJumping = false;
        private bool _isStartJumping = false;
        protected float _jumpVelocity = 0;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _isJumping = false;
            _jumpVelocity = 5f;
            _speed = 10;
        }

        public override void Enter()
        {
            if (_isJumping) return;
            base.Enter();
            _speed = 5;
            _playerController.CharacterMovement.rigidbody.useGravity = true;
            _playerController.CharacterMovement.rigidbody.isKinematic = false;
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _playerController.CharacterMovement.AddForce(JumpDirection(), ForceMode.Force);
            _isStartJumping = true;
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
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            if (_playerController.CharacterMovement.isOnGround && !_isStartJumping)
            {
                _stateContainer.ChangeAction(ActionEnum.Landing);
                return;
            }
            GetInput();
            Rotate();
            MoveInAir();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_isStartJumping)
            {
                _isStartJumping = false;
            }
        }

        public void KeepJumping()
        {
            _state = _displayContainer.PlayAnimation(_keepJumping.Clip, _keepJumping.FadeDuration);
            _state.Speed = _keepJumping.Speed;
            _state.Events = _keepJumping.Events;
        }

        protected override void MoveInAir()
        {
            Vector3 tmp = _moveDirection * _speed;
            tmp.y = _playerController.CharacterMovement.velocity.y;
            _playerController.CharacterMovement.Move(tmp);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _isJumping = false;
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _playerController.CharacterMovement.rigidbody.useGravity = false;
            _playerController.CharacterMovement.rigidbody.isKinematic = true;
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