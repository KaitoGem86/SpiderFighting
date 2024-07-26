using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(menuName = "MyGame/Player/Attack1Action")]
    public class Attack1Action : BaseAttackAction{
        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            _notContinueAttack = false;
            _stateContainer.ChangeAction(ActionEnum.Attack2);
        }
    }
}