using MyTools.Event;
using MyTools.ScreenSystem;
using UnityEngine;

namespace Core.GamePlay.Mission.Protected{
    public class MissionResultPanel : _BaseScreen{
        [SerializeField] private GameObject _successButtons;
        [SerializeField] private GameObject _failButtons;
        [SerializeField] private DefaultEvent _onNextMission;
        [SerializeField] private DefaultEvent _onRetryMission;

        public void OnShow(bool isMissionSuccess){
            _successButtons.SetActive(isMissionSuccess);
            _failButtons.SetActive(!isMissionSuccess);
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