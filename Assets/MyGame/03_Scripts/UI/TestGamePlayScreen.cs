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
        public IntEvent onChangeSkin;
        public DefaultEvent onDodge;
        public DefaultEvent onUltilmateAttack;
        public PlayerController playerController;
        public Core.GamePlay.MyPlayer.PlayerController player;
        public DefaultEvent onAttack;
        private bool _isSwing = false;
        
        public void Update(){
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
            if(Input.GetMouseButtonDown(1)){
                OnClickAttack();    
            }
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                OnCLickDodge();
            }
            if(Input.GetKeyDown(KeyCode.Q)){
                OnClickUltilmateAttack();
            }
        }


        public void OnClickJump()
        {
            InputManager.instance.jump = true;
            StartCoroutine(AfterClickJump());
        }

        public void OnClickChangeSkin(){
            onChangeSkin?.Raise(3);
        }

        public void OnCLickDodge(){
            onDodge?.Raise();
        }

        public void OnClickUltilmateAttack(){
            onUltilmateAttack?.Raise();
        }

        public void OnClickZip(){
            onZip?.Raise();
        }

        public void OnClickSwing(bool Value){
            if(player.blackBoard.Character.IsOnGround()){
                OnClickJump();
                return;
            }
            onSwing?.Raise(value: Value);
            _isSwing = Value;
        }

        public void OnClickAttack(){
            onAttack?.Raise();
        }
    
    
        private IEnumerator AfterClickJump(){
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            InputManager.instance.jump = false;
        }
    }
}