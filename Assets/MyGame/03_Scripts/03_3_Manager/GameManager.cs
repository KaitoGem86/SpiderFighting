using MyTools.Sound;
using UnityEngine;

namespace Core.Manager {
    public class GameManager : MonoBehaviour
    {
        private void Awake(){
            Application.targetFrameRate = 60;
        }

        private void Start(){
            MySoundManager.Instance.PlayMusic();
        }

        public void FocusOnPlayer(bool value){
            Time.timeScale = value ? 0.3f : 1;
        }
    }
}