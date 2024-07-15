using Core.SystemGame;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(StopMovingAction), menuName = ("PlayerState/" + nameof(StopMovingAction)), order = 0)]
    public class StopMovingAction : LocalmotionAction
    {
        [SerializeField] private float _damping = 0f;
        private Vector3 _remainMoveDirection = Vector3.zero;
        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _speed = 5;
            _remainMoveDirection = _playerController.PlayerDisplay.transform.forward * 0.3f;
            _currentTransition.keepAnimation.State.Parameter = _playerController.GetVelocity().magnitude;
        }

        public override void Update()
        {
            base.Update();
            if (InputManager.instance.move.magnitude > 0.1f)
            {
                _stateContainer.ChangeAction(ActionEnum.Moving);
                return;
            }
            if (InputManager.instance.jump)
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
        }

        public override void LateUpdate()
        {
            GetInput();
            _speed = Mathf.Lerp(_speed, 0, _damping * Time.deltaTime);
            if (_speed < 0.1f)
            {
                _speed = 0;
            }
            _moveDirection = _remainMoveDirection;
            base.LateUpdate();
            MoveInAir();
            _remainMoveDirection = Vector3.Lerp(_remainMoveDirection, Vector3.zero, _damping * Time.deltaTime);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public void CheckMoving()
        {
            if (InputManager.instance.move.magnitude > 0.1f)
            {
                _stateContainer.ChangeAction(ActionEnum.Moving);
                return;
            }
            else
            {
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
        }

        protected override void ExitAction()
        {
            CheckMoving();
        }
    }
}