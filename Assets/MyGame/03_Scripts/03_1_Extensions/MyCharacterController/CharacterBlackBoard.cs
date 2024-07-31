using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController{
    public class CharacterBlackBoard<T> : MonoBehaviour where T : MyCharacterController<T>
    {
        public T Controller;
    }
}