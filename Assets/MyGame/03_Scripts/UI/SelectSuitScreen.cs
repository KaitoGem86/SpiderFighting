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

        private List<SkinElement> _skinElements;
        protected override void OnCompleteShowItSelf()
        {
            _skinElementSO.Init(10001, _container);
            _skinElements = _skinElementSO.Spawn(_container);
            OnSelectAnElement(0);
            base.OnCompleteShowItSelf();
        }

        public void OnSelectAnElement(int index)
        {
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
    }
}