using TMPro;
using UnityEngine;

namespace Core.UI{
    public class ShippingQuestPanel : MonoBehaviour{
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private GameObject[] _stepOns;
        [SerializeField] private GameObject[] _stepOffs;

        public void SetTime(float time){
            _timeText.text = time.ToString("0.0");
        }

        public void SetStep(int index){
            for (int i = 0; i < _stepOns.Length; i++){
                _stepOns[i].SetActive(i <= index);
                _stepOffs[i].SetActive(i > index);
            }
        }
    }
}