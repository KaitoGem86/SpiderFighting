using Core.SystemGame;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(IdleAction)), order = 0)]
    public class IdleAction : LocalmotionAction
    {
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            if (actionAfter == ActionEnum.Sprinting) return false;
            return base.Exit(actionAfter);
        }

        public override void LateUpdate()
        {
            if (InputSystem.Instance.InputJoyStick?.Direction.magnitude > 0.1f)
            {
                _stateContainer.ChangeAction(ActionEnum.StartMoving);
                return;
            }
            if (InputSystem.Instance.IsJump)
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
        }


        public override void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Slopes"))
            {
                Vector3 normal = collision.contacts[0].normal;
                float angle = Vector3.Angle(Vector3.up, normal);
            }
        }
    }
}