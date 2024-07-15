using UnityEngine;

namespace Core.GamePlay.Player{
    public interface IPlayerAction : IPlayerLoop{
        public void Enter(ActionEnum beforeAction);
        public bool Exit(ActionEnum actionAfter);
    }
}