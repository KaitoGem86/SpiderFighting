using UnityEngine;
using MyTools.ScreenSystem;
using System.Collections.Generic;
using Core.GamePlay.MyPlayer;
using Core.Manager;
using UnityEngine.UI;
using TMPro;

namespace Progress.UI
{
    public class ProgressScreen : _BaseScreen
    {
        [Header("General")]
        [SerializeField] private Transform _progressContainer;
        [SerializeField] private List<ProgressSO> _progressSOs;
        [SerializeField] private ProgressSO _progressSO;
        [SerializeField] List<Achivement> _achivements;
        
        [Header("Claim Reward")]
        [SerializeField] private GameObject _claimRewardButton;
        [SerializeField] private GameObject _lockIcon;
        
        [Header("NavigationBar")]
        [SerializeField] private List<Image> _navigationButtons;

        [Header("Progress Bar")]
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private TMP_Text _progressLevelText;
        [SerializeField] private TMP_Text _progressNextLevelText;

        private List<ProgressElement> _progressElements = new List<ProgressElement>();
        [SerializeField] private PlayerData _playerData;

        private void Awake()
        {
            
        }

        protected override void OnCompleteShowItSelf()
        {
            base.OnCompleteShowItSelf();
            var _progressSO = _progressSOs[GetIndexProgressSO(_playerData.playerSerializeData.Level)];
            _progressSO.Init(1980, _progressContainer);
            ShowProgress(_progressSO);
        }

        public void ShowProgress(ProgressSO progressSO)
        {
            ShowProgressInfomation();
            foreach (var progressElement in _progressElements)
            {
                _progressSO.DespawnObject(progressElement.gameObject);
            }
            _progressElements.Clear();
            for (int i = 0; i < progressSO.ProgressDatas.Count; i++)
            {
                var progressData = progressSO.ProgressDatas[i];
                var progressElement = _progressSO.Spawn(progressData, _playerData.playerSerializeData.Level, _playerData.playerSerializeData.maxReceiveProgress, _progressSO.levelStart, i).GetComponent<ProgressElement>();
                progressElement.transform.SetParent(_progressContainer);
                _progressElements.Add(progressElement);
            }
            for (int i = 0; i < _achivements.Count; i++)
            {
                _achivements[i].Init(progressSO.AchivementProgresses[i], 0);
            }
            CheckCanCollectMaxReward();
        }

        public void CheckCanCollectMaxReward()
        {
            foreach (var achivement in _achivements)
            {
                if (!achivement.CheckComplete())
                {
                    _claimRewardButton.SetActive(false);
                    _lockIcon.SetActive(true);
                    return;
                }
            }
            _claimRewardButton.SetActive(true);
            _lockIcon.SetActive(false);
        }

        public void Exit()
        {
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.GamePlay);
        }

        private void ShowProgressInfomation(){
            var exp = _playerData.playerSerializeData.Exp;
            var maxProgress = 1000;
            var level = _playerData.playerSerializeData.Level;
            var nextLevel = level + 1;
            _progressSlider.value = exp / maxProgress;
            _progressText.text = $"{exp}/{maxProgress}";
            _progressLevelText.text = "LEVEL " + level.ToString();
            _progressNextLevelText.text = "LEVEL " + nextLevel.ToString();
        }

        private int GetIndexProgressSO(int level)
        {
            for(int i = 0; i < _progressSOs.Count; i++)
            {
                var progressSO = _progressSOs[i];
                if (progressSO.levelStart <= level && progressSO.levelStart + progressSO.ProgressDatas.Count >= level)
                {
                    return i;
                }
            }
            return -1;
        }

        public void OnClaimReward(int level){
            _playerData.playerSerializeData.maxReceiveProgress = level;
            ShowProgress(_progressSOs[GetIndexProgressSO(level)]);
        }
    }
}