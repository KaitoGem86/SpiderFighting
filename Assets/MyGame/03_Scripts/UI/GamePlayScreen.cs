using System.Collections;
using Core.GamePlay.MyPlayer;
using Core.GamePlay.Player;
using Core.GamePlay.Support;
using Core.Manager;
using DamageNumbersPro;
using Data.Stat.Player;
using DG.Tweening;
using Extensions.CooldownButton;
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
        [Header("========== CONTROL UI ==========")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Image _gadgetIcon;
        [SerializeField] private GadgetDataSO _gadgetDataSO;
        [SerializeField] private EventTrigger _lookPanel;
        [SerializeField] private Image _bossIcon;
        [SerializeField] private TMP_Text _bossName;
        [SerializeField] private GameObject _bossHPBar;
        [SerializeField] private CameraFindZipPoint _findZipPoint;
        [SerializeField] private CoolDownButton _ultimateButton;
        [SerializeField] private CoolDownButton _gadgetButton;


        [Header("========== DISPLAY INFO ==========")]
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private HPBarController _hpBarController;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _expText;
        [SerializeField] private TMP_Text _cashText;
        [SerializeField] private TMP_Text _yellowSkinPieceText;
        [SerializeField] private TMP_Text _purpleSkinPieceText;
        [SerializeField] private TMP_Text _skillPointText;
        [SerializeField] private TMP_Text _hitNumber;
        [SerializeField] private Transform _hitNumberParent;

        [Header("========== CONTROL EVENTS ==========")]
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

            GameManager.Instance.playerBlackBoard.CameraFindZipPoint = _findZipPoint;
        }

        protected override void OnCompleteShowItSelf()
        {
            base.OnCompleteShowItSelf();
            UpdatePlayerDisplayData();
            OnChangeEquipGadget(_playerData.playerSerializeData.gadgetIndex);
            _findZipPoint.camera = GameManager.Instance.playerCamera;
            _hitNumber.gameObject.SetActive(false);
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
            _ultimateButton.SetCoolDown(_playerData.playerStatSO.GetGlobalStat(Data.Stat.Player.PlayerStat.UltimateCooldown));
        }

        public void OnClickZip()
        {
            onZip?.Raise();
        }

        public void OnClickScan()
        {
            onUseScan?.Raise();
        }

        public void OnClickCollect()
        {
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

        public void OnShowHitCounter(int hit)
        {
            if (hit == -1)
            {
                _hitNumber.gameObject.SetActive(false);
                return;
            }
            _hitNumber.text = "HITS: x" + hit;
            _hitNumber.gameObject.SetActive(false);
            _hitNumber.gameObject.SetActive(true);
        }

        public void ActiveBossHPBar(CustomEvent.DisplayInfo.DisplayInfo info)
        {
            _bossHPBar.SetActive(true);
            _bossIcon.sprite = info.icon;
            _bossName.text = info.name;
        }

        public void OnClickUseGadget()
        {
            onUseGadget?.Raise();
            PlayerStat stat = _playerData.playerSerializeData.gadgetIndex switch
            {
                0 => PlayerStat.WebShooterCooldown,
                _ => PlayerStat.ConclusiveBlastCooldown,
            };
            Debug.Log("Use Gadget " + stat);
            _playerData.playerSerializeData.lastUseGadgetTime[stat] = System.DateTime.Now;
            float duration = _playerData.playerStatSO.GetGlobalStat(stat);
            _gadgetButton.SetCoolDown(duration);
        }

        public void OnChangeEquipGadget(int id)
        {
            var data = _gadgetDataSO.gadgets[id];
            _gadgetIcon.sprite = data.icon;
            _gadgetButton.StopCoolDown();
            _playerData.playerSerializeData.gadgetIndex = id;
            if (id == 2) return;
            PlayerStat stat = id switch
            {
                0 => PlayerStat.WebShooterCooldown,
                _ => PlayerStat.ConclusiveBlastCooldown,
            };
            double timeFromLastUseGadget = _playerData.GetTimeFromLastUseGadget(stat);
            float duration = _playerData.playerStatSO.GetGlobalStat(stat);
            if (timeFromLastUseGadget >= duration)
            {
                Debug.Log("1 Time from last use gadget " + timeFromLastUseGadget + " duration " + duration);
                _gadgetButton.SetCoolDown(1, duration);
            }
            else{
                Debug.Log("2 Time from last use gadget " + timeFromLastUseGadget + " duration " + duration);
                _gadgetButton.SetCoolDown((duration - (float)timeFromLastUseGadget)/duration, duration);
            }
                
        }

        public void UpdatePlayerDisplayData()
        {
            _hpBarController.SetHP(_playerData.localStats[Data.Stat.Player.PlayerStat.HP], _playerData.playerStatSO.GetGlobalStat(Data.Stat.Player.PlayerStat.HP));
            _levelText.text = "Level : " + _playerData.playerSerializeData.Level;
            _expText.text = "Exp : " + _playerData.playerSerializeData.Exp + "/" + _playerData.GetExpToNextLevel();
            _cashText.text = "Cash : " + _playerData.playerSerializeData.rewards[Data.Reward.RewardType.Cash];
            _yellowSkinPieceText.text = "Yellow Skin Piece : " + _playerData.playerSerializeData.rewards[Data.Reward.RewardType.YellowPiece];
            _purpleSkinPieceText.text = "Purple Skin Piece : " + _playerData.playerSerializeData.rewards[Data.Reward.RewardType.PurplePiece];
            _skillPointText.text = "Skill Point : " + _playerData.playerSerializeData.rewards[Data.Reward.RewardType.SkillPoint];
        }

        private IEnumerator AfterClickJump()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            InputManager.instance.jump = false;
        }
    }
}