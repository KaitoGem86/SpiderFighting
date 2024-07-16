using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(SpawnAction)), order = 0)]
    public class SpawnAction : BasePlayerAction{
        public override void ExitAction()
        {
            base.ExitAction();
            _stateContainer.ChangeAction(ActionEnum.Idle);
        }
    }
}