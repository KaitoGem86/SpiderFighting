using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(menuName = "MyGame/Player/Attack3Action")]
    public class Attack3Action : BaseAttackAction{
        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            _notContinueAttack = false;
            _stateContainer.ChangeAction(ActionEnum.LastAttack);
        }
    }
}