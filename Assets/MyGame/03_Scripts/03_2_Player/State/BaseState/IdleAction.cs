using Core.SystemGame;
using SFRemastered.InputSystem;
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

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            if (actionAfter == ActionEnum.Sprinting) return false;
            return base.Exit(actionAfter);
        }

        public override void Update()
        {
            if(InputManager.instance.jump)
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            if(InputManager.instance.move.magnitude > 0.1f)
            {
                _stateContainer.ChangeAction(ActionEnum.Moving);
                return;
            }
        }
    }
}