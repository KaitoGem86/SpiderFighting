using UnityEngine;

namespace Extentions.SystemGame.MyCharacterController{
    public interface ICharacterAction : ICharacterLoop{
        public void Enter(ActionEnum beforeAction);
        public bool Exit(ActionEnum actionAfter);
    }
}