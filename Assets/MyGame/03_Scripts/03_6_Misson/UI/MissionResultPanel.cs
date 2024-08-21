using MyTools.Event;
using MyTools.ScreenSystem;
using TMPro;
using UnityEngine;

namespace Core.GamePlay.Mission.Protected{
    public class MissionResultPanel : _BaseScreen{
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private TMP_Text _expText;
        [SerializeField] private TMP_Text _cashText;
        [SerializeField] private GameObject _successButtons;
        [SerializeField] private GameObject _failButtons;
        [SerializeField] private DefaultEvent _onNextMission;
        [SerializeField] private DefaultEvent _onRetryMission;

        public void OnShow(bool isMissionSuccess, RewardInfor rewardInfor){
            _resultText.text = isMissionSuccess ? "MISSION SUCCESS" : "MISSION FAILED";
            _successButtons.SetActive(isMissionSuccess);
            _failButtons.SetActive(!isMissionSuccess);
            _expText.text = "+ " + rewardInfor.exp.ToString();
            _cashText.text = "+ " + rewardInfor.currency.ToString();
        }

        public void OnNextMission(){
            _onNextMission.Raise();
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.GamePlay);
        }

        public void OnRetryMission(){
            _onRetryMission.Raise();
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.GamePlay);
        }
    }
}