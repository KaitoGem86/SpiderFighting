using System.Collections;
using Core.GamePlay.MyPlayer;
using Core.GamePlay.Player;
using MyTools.Event;
using MyTools.ScreenSystem;
using SFRemastered.InputSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI{
    public class GamePlayScreen : _BaseScreen
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private GameObject _lookPanel;
        [SerializeField] private Image _gadgetIcon;
        [SerializeField] private GadgetDataSO _gadgetDataSO;
        public DefaultEvent onZip;
        public BoolEvent onSwing;
        public IntEvent onChangeSkin;
        public DefaultEvent onDodge;
        public DefaultEvent onUltilmateAttack;
        public Core.GamePlay.MyPlayer.PlayerController player;
        public DefaultEvent onAttack;
        public DefaultEvent onUseGadget;
        private bool _isSwing = false;

        private void Awake(){
            InputManager.instance.joystickMove = _joystick;
            InputManager.instance.lookPanel = _lookPanel;
        }
        
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
            if(Input.GetKeyDown(KeyCode.E)){
                OnClickUseGadget();
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

        public void OnClickOpenSkin(){
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.Selection);
        }

        public void OnClickOpenProgress(){
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.Progress);
        }

        public void OnClickSwing(bool Value){
            // if(player.blackBoard.Character.IsOnGround()){
            //     OnClickJump();
            //     return;
            // }
            onSwing?.Raise(value: Value);
            _isSwing = Value;
        }

        public void OnClickAttack(){
            onAttack?.Raise();
        }

        public void OnClickUseGadget(){
            onUseGadget?.Raise();
        }

        public void OnChangeEquipGadget(int id){
            var data = _gadgetDataSO.gadgets[id];
            _gadgetIcon.sprite = data.icon;
        }
    
    
        private IEnumerator AfterClickJump(){
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            InputManager.instance.jump = false;
        }
    }
}