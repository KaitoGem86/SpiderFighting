using MyTools.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.GamePlay.MyPlayer{
    public class GadgetElement : MonoBehaviour {
        [SerializeField] Image _background;
        [SerializeField] Image _icon;
        [SerializeField] TMP_Text _name;
        [SerializeField] IntEvent _onGadgetSelected;
        public RectTransform controlRect; 

        private GadgetData _data;

        public void Init(GadgetData data){
            _data = data;
            _icon.sprite = data.icon;
            _name.text = data.name;
            _background.sprite = data.DefaultBG;
        }

        public void OnClick(){
            Debug.Log("GadgetElement.OnClick " + _data.name + " " + _data.id);
            _onGadgetSelected?.Raise(_data.id);
        }

        public void OnSelect(bool isSelected){
            _background.sprite = isSelected ? _data.SelectedBG : _data.DefaultBG;
        }

        public TMP_Text Name => _name;
        public int Id => _data.id;
    }
}