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
        private float _jumpVelocity = 0;
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
            _playerController.CharacterMovement.AddForce(Vector3.up * _jumpVelocity, ForceMode.Force);
            Debug.Log(_playerController.CharacterMovement.velocity.y);
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
            GetInput();
            Rotate();
            MoveInAir();
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