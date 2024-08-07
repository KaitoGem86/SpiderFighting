using System.Collections.Generic;
using Animancer;
using Core.GamePlay.Support;
using MyTools.ScreenSystem;
using UnityEngine;

namespace Core.UI
{
    public class SelectSuitScreen : _BaseScreen
    {
        [SerializeField] private SkinElementSO _skinElementSO;
        [SerializeField] private Transform _container;
        [SerializeField] private PreviewPlayerModels _previewPlayerModels;
        [SerializeField] private GameObject _suitPanel;
        [SerializeField] private GameObject _gadgetPanel;
        [SerializeField] private GameObject _skillPanel;

        private List<SkinElement> _skinElements;

        private void OnEnable(){
            OnSelectSuit();
        }

        protected override void OnCompleteShowItSelf()
        {
            _skinElementSO.Init(10001, _container);
            _skinElements = _skinElementSO.Spawn(_container);
            OnSelectAnElement(0);
            base.OnCompleteShowItSelf();
        }

        public void OnSelectAnElement(int index)
        {
            if (_skinElements == null || _skinElements.Count == 0) return;
            foreach (var skinElement in _skinElements)
            {
                skinElement.SetSelect(false);
            }
            _skinElements[index].SetSelect(true);
            _previewPlayerModels.PreviewModel(index);
        }

        public void Exit(){
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.GamePlay);
        }

        public void OnSelectSuit(){
            if(_suitPanel.activeSelf) return;
            _suitPanel.SetActive(true);
            OnSelectAnElement(0);
            _gadgetPanel.SetActive(false);
           // _skillPanel.SetActive(false);
        }

        public void OnSelectGadget(){
            if(_gadgetPanel.activeSelf) return;
            _suitPanel.SetActive(false);
            _gadgetPanel.SetActive(true);
            //_skillPanel.SetActive(false);
        }

        public void OnSelectSkill(){
            if(_skillPanel.activeSelf) return;
            _suitPanel.SetActive(false);
            _gadgetPanel.SetActive(false);
           // _skillPanel.SetActive(true);
        }
    }
}