using System.Collections.Generic;
using Core.GamePlay.Support;
using MyTools.ScreenSystem;
using UnityEngine;

namespace Core.UI
{
    public class SelectSuitScreen : _BaseScreen
    {
        [SerializeField] private SkinElementSO _skinElementSO;
        [SerializeField] private Transform _container;
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
        }
    }
}