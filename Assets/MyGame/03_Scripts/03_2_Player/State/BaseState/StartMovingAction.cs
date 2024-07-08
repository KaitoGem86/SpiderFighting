using Core.SystemGame;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(StartMovingAction), menuName = ("PlayerState/" + nameof(StartMovingAction)), order = 0)]
    public class StartMovingAction : MovingAction
    {
        [SerializeField] private float _damping = 0f;
        private float _targetSpeed = 0;

        public override void Enter()
        {
            base.Enter();
            _targetSpeed = 5;
            _speed = 0;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public override void LateUpdate()
        {
            if (InputSystem.Instance.InputJoyStick.Direction.magnitude < 0.1f)
            {
                _stateContainer.ChangeAction(ActionEnum.StopMoving);
                return;
            }
            if (InputSystem.Instance.IsJump)
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            _speed = Mathf.Lerp(_speed, _targetSpeed, _damping * Time.deltaTime);
            if (_targetSpeed - _speed < 0.1f)
            {
                Debug.Log("CheckMoving");
                _speed = _targetSpeed;
            }
            GetInput();
            var rayCheckGround = RaycastCheckGround();
            if (!rayCheckGround.Item1 && rayCheckGround.Item2 > 3)
            {
                _stateContainer.ChangeAction(ActionEnum.FallingDown);
                return;
            }
            MoveInAir();
            Rotate();
        }

        public void CheckMoving()
        {
            if (InputSystem.Instance.InputJoyStick.Direction.magnitude > 0.1f)
            {
                if (InputSystem.Instance.IsSprint)
                {
                    _stateContainer.ChangeAction(ActionEnum.Sprinting);
                    return;
                }
                else
                {
                    _stateContainer.ChangeAction(ActionEnum.Moving);
                    return;
                }
            }
            else
            {
                _stateContainer.ChangeAction(ActionEnum.StopMoving);
                return;
            }
        }
    }
}