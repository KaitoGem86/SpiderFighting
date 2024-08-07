using MyTools.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.GamePlay.MyPlayer{
    public class GadgetElement : MonoBehaviour {
        [SerializeField] Image _icon;
        [SerializeField] TMP_Text _name;
        [SerializeField] IntEvent _onGadgetSelected;

        private GadgetData _data;

        public void Init(GadgetData data){
            _data = data;
            _icon.sprite = data.icon;
            _name.text = data.name;
        }

        public void OnClick(){
            Debug.Log("GadgetElement.OnClick " + _data.name + " " + _data.id);
            _onGadgetSelected?.Raise(_data.id);
        }
    }
}