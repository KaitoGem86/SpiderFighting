using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(DieAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(DieAction)), order = 0)]
    public class DieAction : BasePlayerAction{
        public override void Enter()
        {
            base.Enter();
        }
    }
}