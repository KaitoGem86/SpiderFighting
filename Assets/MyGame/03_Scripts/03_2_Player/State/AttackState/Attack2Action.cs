using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(menuName = "MyGame/Player/Attack2Action")]
    public class Attack2Action : BaseAttackAction{
        public override void Attack()
        {
            if (!_isCanChangeNextAttack) return;
            _notContinueAttack = false;
            _stateContainer.ChangeAction(ActionEnum.Attack3);
        }
    }
}