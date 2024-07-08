using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(PickUpAction), menuName = ("PlayerState/" + nameof(PickUpAction)), order = 0)]
    public class PickUpAction : BaseInteractionAction
    {
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter()
        {
            if (_stateContainer.GetAction(_stateContainer.CurrentMovementAction).Priortiy == PriorityEnum.Critical)
                return;
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public void CompletePickUp()
        {
            _stateContainer.ChangeAction(ActionEnum.None);
            if (_stateContainer.GetAction(_stateContainer.CurrentMovementAction).Priortiy <= this.Priortiy)
                _stateContainer.ChangeAction(ActionEnum.Idle);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }
    }
}