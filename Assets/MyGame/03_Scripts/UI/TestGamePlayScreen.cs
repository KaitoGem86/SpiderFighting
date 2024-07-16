using System.Collections;
using Core.GamePlay.Player;
using MyTools.Event;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.UI{
    public class TestGamePlayScreen : MonoBehaviour
    {
        public DefaultEvent onZip;
        public BoolEvent onSwing;
        public PlayerController playerController;

        private bool _isSwing = false;
        
        public void Update(){
            if(Input.GetKeyDown(KeyCode.C)){
                OnClickJump();
            }
            if(Input.GetKeyDown(KeyCode.Z)){
                OnClickZip();
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                OnClickSwing(true);
            }
            if(Input.GetKeyUp(KeyCode.Space)){
                OnClickSwing(false);
            }
            if(_isSwing){
                onSwing?.Raise(value: true);
            }
        }


        public void OnClickJump()
        {
            InputManager.instance.jump = true;
            StartCoroutine(AfterClickJump());
        }

        public void OnClickZip(){
            onZip?.Raise();
        }

        public void OnClickSwing(bool Value){
            if(playerController.IsOnGround()){
                OnClickJump();
                return;
            }
            onSwing?.Raise(value: Value);
            _isSwing = Value;
        }
    
    
        private IEnumerator AfterClickJump(){
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            InputManager.instance.jump = false;
        }
    }
}