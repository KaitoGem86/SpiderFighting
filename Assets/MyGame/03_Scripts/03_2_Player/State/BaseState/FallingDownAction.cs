using Core.GamePlay.Player;
using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(FallingDownAction)), order = 0)]
    public class FallingDownAction : InAirAction
    {
        [SerializeField] private float _velocityThreshHold = 2f;
        [SerializeField] BoolSerializeEventListener _onFallingDown;
        
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _onFallingDown.RegisterListener();
            if (beforeAction == ActionEnum.Climbing)
            {
                _playerController.SetVelocity(Vector3.up * 10);
            }
            _speed = _playerController.GlobalVelocity.magnitude;
        }

        public override void Update()
        {
            base.Update();
            if(_playerController.GetVelocity().magnitude > _velocityThreshHold)
            {
                _stateContainer.ChangeAction(ActionEnum.Dive);
                return;
            }

            if (_playerController.CharacterMovement.isOnGround)
            {
                _stateContainer.ChangeAction(ActionEnum.Landing);
                return;
            }
        }

        public void ChangeToSwing(bool isSwing){
            if(isSwing){
                _stateContainer.ChangeAction(ActionEnum.Swing);
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onFallingDown.UnregisterListener();
            return base.Exit(actionAfter);
        }
    }
}