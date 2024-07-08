using UnityEngine;

namespace MyTools.PopupSystem{
    public class BasePopup : MonoBehaviour{
        [SerializeField] private PopupTypeEnum _popupType;

        public bool IsShow {get ; private set;}
        public PopupTypeEnum PopupType => _popupType;

        public void ShowPopup(){}
    }
}