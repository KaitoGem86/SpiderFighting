using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(NoneAction), menuName = ("PlayerState/" + nameof(NoneAction)), order = 0)]
    public class NoneAction : BaseInteractionAction
    {
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter()
        {
            _displayContainer.StopAnimation(PlayerTypeAnimMask.Upper);
            _displayContainer.StopAnimation(PlayerTypeAnimMask.Lower);
            if(_stateContainer.CurrentMovementAction == ActionEnum.Idle){
                _displayContainer.PlayAnimation(_animationClip.Clip, _animationClip.FadeDuration, PlayerTypeAnimMask.Base);
            }
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

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }
    }
}