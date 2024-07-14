using UnityEngine;

namespace Core.Manager {
    public class GameManager : MonoBehaviour
    {
        public void FocusOnPlayer(bool value){
            Time.timeScale = value ? 0.3f : 1;
        }
    }
}