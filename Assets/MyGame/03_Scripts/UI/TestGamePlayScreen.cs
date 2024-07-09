using System.Collections;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.UI{
    public class TestGamePlayScreen : MonoBehaviour
    {
        public void Update(){
            if(Input.GetKeyDown(KeyCode.Space)){
                OnClickJump();
            }
        }


        public void OnClickJump()
        {
            InputManager.instance.jump = true;
            StartCoroutine(AfterClickJump());
        }
    
    
        private IEnumerator AfterClickJump(){
            yield return new WaitForEndOfFrame();
            InputManager.instance.jump = false;
        }
    }
}