using System;
using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(JumpFromSwingAciton), menuName = ("PlayerState/" + nameof(JumpFromSwingAciton)), order = 0)]
    public class JumpFromSwingAciton : JumpingAction{
        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _playerController.SetVelocity(JumpDirection());
        }

        protected override Vector3 JumpDirection()
        {
            _jumpVelocity = 5f;
            return _playerController.CharacterMovement.velocity + Vector3.up * _jumpVelocity;
        }
    }
}