using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController{
    public interface ICharacterAction : ICharacterLoop{
        public void Enter(ActionEnum beforeAction);
        public bool Exit(ActionEnum actionAfter);
    }
}