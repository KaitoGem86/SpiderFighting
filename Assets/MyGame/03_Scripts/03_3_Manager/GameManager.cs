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

        private void Update(){
            if(Input.GetKeyDown(KeyCode.F)){
                FocusOnPlayer(true);
            }
            if(Input.GetKeyUp(KeyCode.F)){
                FocusOnPlayer(false);
            }
        }

        public void FocusOnPlayer(bool value){
            Time.timeScale = value ? 0.3f : 1;
        }
    }
}