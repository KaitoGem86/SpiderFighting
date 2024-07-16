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
            _playerController.SetMovementMode(EasyCharacterMovement.MovementMode.Falling);
            _playerController.SetVelocity(_playerController.GlobalVelocity);
            if (beforeAction == ActionEnum.Climbing)
            {
                _playerController.SetVelocity(Vector3.up);
            }
            _speed = 10;
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
            Debug.Log("Change to Swing");
            if(isSwing){
                _stateContainer.ChangeAction(ActionEnum.Swing);
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onFallingDown.UnregisterListener();
            _playerController.GlobalVelocity = _playerController.GetVelocity();
            return base.Exit(actionAfter);
        }
    }
}