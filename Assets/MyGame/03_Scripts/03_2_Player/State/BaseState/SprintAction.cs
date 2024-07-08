using Animancer;
using Core.SystemGame;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(SprintAction)), order = 0)]
    public class SprintAction : MovingAction
    {
        [SerializeField] private LinearMixerTransition _sprintTransition;
        private ManualMixerState _sprintState;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _speed = 5f;
        }

        public override void Enter()
        {
            base.Enter();
            _state = _displayContainer.PlayAnimation(_sprintTransition, _sprintTransition.FadeDuration);
            _speed = 8f;
        }


        public override void Update()
        {
            if (!InputSystem.Instance.IsSprint)
            {
                _stateContainer.ChangeAction(ActionEnum.Moving);
                return;
            }
            base.Update();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            _sprintTransition.State.Parameter = _moveDirection.magnitude;
        }



        public override bool Exit(ActionEnum actionAfter)
        {
            //_displayContainer.StopAnimation(PlayerTypeAnimMask.Base);
            return base.Exit(actionAfter);
        }
    }
}