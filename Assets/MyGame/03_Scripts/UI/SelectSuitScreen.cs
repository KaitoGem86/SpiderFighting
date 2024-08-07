using System.Collections.Generic;
using Core.GamePlay.Support;
using UnityEngine;

namespace Core.UI{
    public class SelectSuitScreen : MonoBehaviour{
        [SerializeField] private SkinElementSO _skinElementSO;
        [SerializeField] private Transform _container;
        private List<SkinElement> _skinElements;

        private void Awake(){
            _skinElementSO.Init(10001, _container);
        }

        private void OnEnable(){
           _skinElements = _skinElementSO.Spawn();
           OnSelectAnElement(0);
        }

        private void OnSelectAnElement(int index){
            foreach (var skinElement in _skinElements){
                skinElement.SetSelect(false);
            }
            _skinElements[index].SetSelect(true);
        }
    }
}