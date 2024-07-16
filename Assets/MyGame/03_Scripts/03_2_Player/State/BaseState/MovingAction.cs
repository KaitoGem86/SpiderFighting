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

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
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
                _stateContainer.ChangeAction(ActionEnum.FallingDown);
                return;
            }
            GetInput();
            if (_moveDirection.Equals(Vector3.zero))
            {
                OnDontMove();
            }
        }

        public override void LateUpdate()
        {
            Move();
            Rotate();
            _currentTransition.keepAnimation.State.Parameter = _moveDirection.magnitude * 2;
        }

        protected virtual void OnDontMove()
        {
            _stateContainer.ChangeAction(ActionEnum.StopMoving);
        }
    }
}