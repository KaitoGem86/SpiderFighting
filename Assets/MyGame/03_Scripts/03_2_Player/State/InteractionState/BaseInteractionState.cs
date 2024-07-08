using System;
using UnityEngine;

namespace Core.GamePlay.Player
{
    public class BaseInteractionAction : BasePlayerAction
    {
        [SerializeField] private PlayerTypeAnimMask _actionTypeAnimLayer;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter()
        {
            ActionEnum currentMovementAction = _stateContainer.CurrentMovementAction;
            BasePlayerAction basePlayerAction = _stateContainer.GetAction(currentMovementAction);
            // if (IsHigherPriority(basePlayerAction))
            // {
            //     _state = _displayContainer.PlayAnimation(_animationClip.Clip, _animationClip.FadeDuration, PlayerTypeAnimMask.Base);
            //     CurrentAnimLayer = PlayerTypeAnimMask.Base;
            // }
            // else{
            //     _state = _displayContainer.PlayAnimation(_animationClip.Clip, _animationClip.FadeDuration, _actionTypeAnimLayer);
            //     CurrentAnimLayer = _actionTypeAnimLayer;
            // }
            _state = _displayContainer.PlayAnimation(_animationClip.Clip, _animationClip.FadeDuration, _actionTypeAnimLayer);
            _state.Events = _animationClip.Events;
            _state.Speed = _animationClip.Speed;
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            if(actionAfter == ActionEnum.Holding && _stateContainer.CurrentMovementAction == ActionEnum.Cooking)
            {
                _state = null;
                return false;
            }
            return base.Exit(actionAfter);
        }

        private bool IsHigherPriority(BasePlayerAction action)
        {
            return Priortiy >= action.Priortiy;
        }

        public PlayerTypeAnimMask CurrentAnimLayer {get; set;}
    }

}