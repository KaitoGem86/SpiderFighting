using Core.GamePlay.Player;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(FallingDownAction)), order = 0)]
    public class FallingDownAction : LocalmotionAction
    {
        [SerializeField] private float _heightThreshHold = 2f;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter()
        {
            _playerController.CharacterMovement.rigidbody.useGravity = true;
            _playerController.CharacterMovement.rigidbody.isKinematic = false;
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            var tmp = RaycastCheckGround();
            if (tmp.Item2 > _heightThreshHold)
                _priority = PriorityEnum.Critical;
            else
                _priority = PriorityEnum.High;
            if (_stateContainer.CurrentInteractionAction == ActionEnum.None || _priority != PriorityEnum.Critical)
                base.Enter();
            _speed = 5;
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
            if (_playerController.CharacterMovement.isOnGround)
            {
                _stateContainer.ChangeAction(ActionEnum.Landing);
                return;
            }
            GetInput();
            if (_rotateDirection != Vector3.zero)
                Rotate();
            MoveInAir();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _playerController.CharacterMovement.rigidbody.useGravity = false;
            _playerController.CharacterMovement.rigidbody.isKinematic = true;
            return base.Exit(actionAfter);
        }
    }
}