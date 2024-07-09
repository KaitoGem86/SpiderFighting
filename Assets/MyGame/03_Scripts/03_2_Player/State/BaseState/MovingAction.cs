using Animancer;
using Core.SystemGame;
using DG.Tweening;
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
            //base.Enter();
            _state = _displayContainer.PlayAnimation(_linearMixerTransition, _linearMixerTransition.FadeDuration);
            _playerController.IsCanMove = true;
            //_moveDirection = Vector3.zero;
            GetInput();
            _speed = 8f;
            // if (_playerController.IsSprinting)
            // {
            //     _stateContainer.ChangeAction(ActionEnum.Sprinting);
            // }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public override void LateUpdate()
        {
            //);
            // if (InputSystem.Instance.IsJump)
            // {
            //     if (_stateContainer.ChangeAction(ActionEnum.Jumping))
            //         return;
            // }
            // else if (InputSystem.Instance.IsSprint)
            // {
            //     if (_stateContainer.ChangeAction(ActionEnum.Sprinting))
            //         return;
            // }
            GetInput();
            if (_moveDirection.Equals(Vector3.zero))
            {
                OnDontMove();
            }

            // var rayCheckGround = RaycastCheckGround();
            // if (!rayCheckGround.Item1 && rayCheckGround.Item2 > 3)
            // {
            //     _stateContainer.ChangeAction(ActionEnum.FallingDown);
            //     return;
            // }
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