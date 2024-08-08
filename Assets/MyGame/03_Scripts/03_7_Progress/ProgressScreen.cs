using UnityEngine;
using MyTools.ScreenSystem;
using System.Collections.Generic;

namespace Progress.UI{
    public class ProgressScreen : _BaseScreen{
        [SerializeField] private Transform _progressContainer;
        [SerializeField] private ProgressSO _progressSO;

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
        }
    }
}