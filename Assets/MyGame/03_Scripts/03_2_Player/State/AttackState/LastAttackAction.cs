using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(menuName = "MyGame/Player/LastAttackAction")]
    public class LastAttackAction : BaseAttackAction{
        public override void KeepAction()
        {
            base.KeepAction();
            _currentTransition.keepAnimation.State.Parameter = Random.Range(0,3);
        }
    }
}