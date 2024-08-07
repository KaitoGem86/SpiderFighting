using MyTools.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Core.GamePlay.Support{
    [System.Serializable]
    public class SkinData {
        public Sprite defaultBackground;
        public Sprite selectBackground;
        public Sprite avatar;
        public int index;
    }
    
    public class SkinElement : MonoBehaviour{
        [SerializeField] private Image _background;
        [SerializeField] private Image _avatar;
        [SerializeField] private IntEvent _onChangeSkin;
        private SkinData _skinData;

        public void Init(SkinData skinData){
            _skinData = skinData;
            _background.sprite = skinData.defaultBackground;
            _avatar.sprite = skinData.avatar;
        } 

        public void SetSelect(bool isSelect){
            _background.sprite = isSelect ? _skinData.selectBackground : _skinData.defaultBackground;
        }

        public void OnClick(){
            _onChangeSkin?.Raise(_skinData.index);
        }
    }
}