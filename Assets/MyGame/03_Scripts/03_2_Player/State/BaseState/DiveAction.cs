using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(DiveAction)), order = 0)]
    public class DiveAction : InAirAction
    {
        [SerializeField] private BoolSerializeEventListener _onSwing;

        private bool _isCanChangeToSwing = false;
        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _onSwing.RegisterListener();
            _speed = _playerController.GlobalVelocity.magnitude;
            _isCanChangeToSwing = false;
        }

        public override void Update()
        {
            if (_playerController.CharacterMovement.isOnGround)
            {
                _stateContainer.ChangeAction(ActionEnum.Landing);
                return;
            }
            base.Update();
        }

        public override void KeepAction()
        {
            base.KeepAction();
            _isCanChangeToSwing = true;
        }

        public void ChangeToSwing(bool isSwing)
        {
            if (!_isCanChangeToSwing)
            {
                return;
            }
            if (isSwing)
            {
                _stateContainer.ChangeAction(ActionEnum.Swing);
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onSwing.UnregisterListener();
            return base.Exit(actionAfter);
        }

        protected override void GetInput()
        {
            base.GetInput();
            if (_moveDirection == Vector3.zero)
            {
                _moveDirection = _playerController.CameraTransform.forward;
                _moveDirection.y = 0;
                _moveDirection = _moveDirection.normalized;
            }
            if (_rotateDirection == Vector3.zero)
            {
                _rotateDirection = _playerController.CameraTransform.forward;
                _rotateDirection.y = 0;
                _rotateDirection = _rotateDirection.normalized;
            }
        }
    }
}