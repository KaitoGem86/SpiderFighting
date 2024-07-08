using UnityEngine;

namespace Core.GamePlay.Player
{

    [CreateAssetMenu(fileName = nameof(SlideAction), menuName = ("PlayerState/" + nameof(SlideAction)), order = 0)]
    public class SlideAction : LocalmotionAction
    {
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);

        }

        public override void Enter()
        {
            var raycastCheck = RaycastCheckGround();
            if (!raycastCheck.Item1)
            {
                _stateContainer.ChangeAction(ActionEnum.FallingDown);
                return;
            }
            if (Vector3.Angle(_surfaceNormal, Vector3.up) < _maxAngularSlopes)
            {
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
            base.Enter();
            _speed = 2;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            var raycastCheck = RaycastCheckGround();
            if (!raycastCheck.Item1)
            {
                _stateContainer.ChangeAction(ActionEnum.FallingDown);
                return;
            }
            if (Vector3.Angle(_surfaceNormal, Vector3.up) < _maxAngularSlopes)
            {
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
            GetInput();
            if (Vector3.Angle(_surfaceNormal, _rotateDirection) <= 90) {
                Debug.Log("Slide");
            }
            else
            {
                Vector3 temp = Vector3.Cross(Vector3.up, _surfaceNormal);
                _moveDirection = -Vector3.Cross(_surfaceNormal, temp).normalized;
                _rotateDirection = -Vector3.Cross(Vector3.up, temp).normalized;
            }
            MoveInAir();
            Rotate();
        }

        public override void FixedUpdate()
        {

        }

        public override bool Exit(ActionEnum actionAfter)
        {

            return base.Exit(actionAfter);
        }
    }
}