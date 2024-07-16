using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(DiveAction)), order = 0)]
    public class DiveAction : InAirAction{
        [SerializeField] private BoolSerializeEventListener _onSwing;
        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _onSwing.RegisterListener();
            _speed = 25;
        }

        public override void Update()
        {
            if(_playerController.CharacterMovement.isOnGround){
                _stateContainer.ChangeAction(ActionEnum.Landing);
                return;
            }
            base.Update();
        }

        public void ChangeToSwing(bool isSwing){
            if(isSwing){
                _stateContainer.ChangeAction(ActionEnum.Swing);
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onSwing.UnregisterListener();
            _playerController.GlobalVelocity = _playerController.GetVelocity();
            return base.Exit(actionAfter);
        }
    }
}