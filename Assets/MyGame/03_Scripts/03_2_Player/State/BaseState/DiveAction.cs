using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(DiveAction)), order = 0)]
    public class DiveAction : InAirAction{
        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _speed = 8;
        }

        public override void Update()
        {
            if(_playerController.CharacterMovement.isOnGround){
                _stateContainer.ChangeAction(ActionEnum.Landing);
                return;
            }
            if(Input.GetKey(KeyCode.Space)){
                _stateContainer.ChangeAction(ActionEnum.Swing);
            }
            base.Update();
        }
    }
}