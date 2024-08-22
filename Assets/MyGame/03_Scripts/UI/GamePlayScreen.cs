using System.Collections;
using Core.GamePlay.MyPlayer;
using Core.GamePlay.Player;
using Core.GamePlay.Support;
using Core.Manager;
using MyTools.Event;
using MyTools.ScreenSystem;
using SFRemastered.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class GamePlayScreen : _BaseScreen
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Image _gadgetIcon;
        [SerializeField] private GadgetDataSO _gadgetDataSO;
        [SerializeField] private EventTrigger _lookPanel;
        [SerializeField] private Image _bossIcon;
        [SerializeField] private TMP_Text _bossName;
        [SerializeField] private GameObject _bossHPBar;
        [SerializeField] private CameraFindZipPoint _findZipPoint;
        public DefaultEvent onZip;
        public BoolEvent onSwing;
        public IntEvent onChangeSkin;
        public DefaultEvent onDodge;
        public DefaultEvent onUltilmateAttack;
        public DefaultEvent onAttack;
        public DefaultEvent onUseGadget;
        public DefaultEvent onUseScan;
        public DefaultEvent onCollect;
        private bool _isSwing = false;

        private void Awake()
        {
            InputManager.instance.joystickMove = _joystick;
            Debug.Log("GamePlayScreen Awake " + _lookPanel.name);
            InputManager.instance.lookPanel = _lookPanel.gameObject;
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { InputManager.instance.LookPressed(data); });
            _lookPanel.triggers.Add(entry);

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerUp;
            entry2.callback.AddListener((data2) => { InputManager.instance.LookReleased(); });
            _lookPanel.triggers.Add(entry2);
        }

        protected override void OnCompleteShowItSelf()
        {
            base.OnCompleteShowItSelf();
            _findZipPoint.camera = GameManager.Instance.playerCamera;
        }

        public void Update()
        {
            
            
            if (_isSwing)
            {
                onSwing?.Raise(value: true);
            }
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnClickSwing(true);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                OnClickSwing(false);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OnClickZip();
            }
            if (Input.GetMouseButtonDown(1))
            {
                OnClickAttack();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                OnCLickDodge();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnClickUltilmateAttack();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnClickUseGadget();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                OnClickScan();
            }
#endif     
        }


        public void OnClickJump()
        {
            InputManager.instance.jump = true;
            StartCoroutine(AfterClickJump());
        }

        public void OnClickChangeSkin()
        {
            onChangeSkin?.Raise(3);
        }

        public void OnCLickDodge()
        {
            onDodge?.Raise();
        }

        public void OnClickUltilmateAttack()
        {
            onUltilmateAttack?.Raise();
        }

        public void OnClickZip()
        {
            onZip?.Raise();
        }

        public void OnClickScan()
        {
            onUseScan?.Raise();
        }

        public void OnClickCollect(){
            onCollect?.Raise();
        }

        public void OnClickOpenSkin()
        {
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.Selection);
        }

        public void OnClickOpenProgress()
        {
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.Progress);
        }

        public void OnClickOpenDailyReward()
        {
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.DailyReward);
        }

        public void OnClickSwing(bool Value)
        {
            // if(player.blackBoard.Character.IsOnGround()){
            //     OnClickJump();
            //     return;
            // }
            onSwing?.Raise(value: Value);
            _isSwing = Value;
        }

        public void OnClickAttack()
        {
            onAttack?.Raise();
        }

        public void ActiveBossHPBar(CustomEvent.DisplayInfo.DisplayInfo info){
            _bossHPBar.SetActive(true);
            _bossIcon.sprite = info.icon;
            _bossName.text = info.name;
        }

        public void OnClickUseGadget()
        {
            onUseGadget?.Raise();
        }

        public void OnChangeEquipGadget(int id)
        {
            var data = _gadgetDataSO.gadgets[id];
            _gadgetIcon.sprite = data.icon;
        }


        private IEnumerator AfterClickJump()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            InputManager.instance.jump = false;
        }
    }
}