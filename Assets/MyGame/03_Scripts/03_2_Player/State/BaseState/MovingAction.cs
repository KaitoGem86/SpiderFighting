using Animancer;
using Core.SystemGame;
using DG.Tweening;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(MovingAction)), order = 0)]
    public class MovingAction : LocalmotionAction
    {

        [SerializeField] private LinearMixerTransition _linearMixerTransition;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _speed = 4f;
        }

        public override void Enter()
        {
            _state = _displayContainer.PlayAnimation(_linearMixerTransition, _linearMixerTransition.FadeDuration);
            _playerController.IsCanMove = true;
            GetInput();
            _speed = 8f;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public override void Update()
        {
            if(InputManager.instance.jump)
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            if(!_playerController.CharacterMovement.isOnGround)
            {
                Debug.Log("Falling Down");
                _stateContainer.ChangeAction(ActionEnum.FallingDown);
                return;
            }
        }

        public override void LateUpdate()
        {
            GetInput();
            if (_moveDirection.Equals(Vector3.zero))
            {
                OnDontMove();
            }
            MoveInAir();
            Rotate();
            _linearMixerTransition.State.Parameter = _moveDirection.magnitude * 2;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        public override void OnCollisionEnter(Collision collision)
        {
        }

        public override void OnCollisionExit(Collision collision)
        {
        }

        public override void OnCollisionStay(Collision collision)
        {
        }

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }

        protected virtual void OnDontMove()
        {
            _stateContainer.ChangeAction(ActionEnum.StopMoving);
        }
    }
}