using UnityEngine;

namespace Core.GamePlay.Player{
    public interface IPlayerAction : IPlayerLoop{
        public void Enter();
        public bool Exit(ActionEnum actionAfter);
    }
}