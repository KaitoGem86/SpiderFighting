using TMPro;
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class ConfigElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_InputField _inputField;
        private string _inputText;
        StatData _data;

        public void Init(StatData data)
        {
            _title.text = data.type.ToString();
            _inputField.text = data.value.ToString();
        }

        public void OnInputFieldChange(string value)
        {
            _inputText = value;
        }

        public void OnClickAccept(){

        }
    }
}