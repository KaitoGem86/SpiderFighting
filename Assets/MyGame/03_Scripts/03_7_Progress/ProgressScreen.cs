using UnityEngine;
using MyTools.ScreenSystem;
using System.Collections.Generic;
using Core.GamePlay.MyPlayer;

namespace Progress.UI{
    public class ProgressScreen : _BaseScreen{
        [SerializeField] private Transform _progressContainer;
        [SerializeField] private ProgressSO _progressSO;
        [SerializeField] List<Achivement> _achivements;
        [SerializeField] private GameObject _claimRewardButton;
        [SerializeField] private GameObject _lockIcon;

        private List<ProgressElement> _progressElements = new List<ProgressElement>();

        private void Awake(){
            _progressSO.Init(1980, _progressContainer);
        }

        protected override void OnCompleteShowItSelf()
        {
            base.OnCompleteShowItSelf();
            ShowProgress(_progressSO);
        }

        public void ShowProgress(ProgressSO progressSO){
            foreach (var progressElement in _progressElements)
            {
                _progressSO.DespawnObject(progressElement.gameObject);
            }
            _progressElements.Clear();
            foreach (var progressData in progressSO.ProgressDatas)
            {
                var progressElement = _progressSO.Spawn(progressData).GetComponent<ProgressElement>();
                progressElement.transform.SetParent(_progressContainer);
                _progressElements.Add(progressElement);
            }
            for(int i = 0; i < _achivements.Count; i++){
                _achivements[i].Init(progressSO.AchivementProgresses[i], 0);
            }
            CheckCanCollectMaxReward();
        }

        public void CheckCanCollectMaxReward(){
            foreach (var achivement in _achivements)
            {
                if(!achivement.CheckComplete()){
                    _claimRewardButton.SetActive(false);
                    _lockIcon.SetActive(true);
                    return;
                }
            }
            _claimRewardButton.SetActive(true);
            _lockIcon.SetActive(false);
        }

        public void Exit(){
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.GamePlay);
        }
    }
}