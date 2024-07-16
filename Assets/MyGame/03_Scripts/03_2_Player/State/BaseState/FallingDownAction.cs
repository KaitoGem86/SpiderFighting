using Core.GamePlay.Player;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(FallingDownAction)), order = 0)]
    public class FallingDownAction : InAirAction
    {
        [SerializeField] private float _velocityThreshHold = 2f;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _speed = 5;
        }

        public override void Update()
        {
            base.Update();
            if(_playerController.GetVelocity().magnitude > _velocityThreshHold)
            {
                _stateContainer.ChangeAction(ActionEnum.Dive);
                return;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _stateContainer.ChangeAction(ActionEnum.Swing);
            }

            if (_playerController.CharacterMovement.isOnGround)
            {
                _stateContainer.ChangeAction(ActionEnum.Landing);
                return;
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }
    }
}