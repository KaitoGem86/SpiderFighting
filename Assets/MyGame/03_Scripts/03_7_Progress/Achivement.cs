using Core.GamePlay.MyPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Progress
{
    public class Achivement : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private AchivementType _achivementType;
        [SerializeField] private TMP_Text _targetAmount;
        [SerializeField] private TMP_Text _name;
        private int _targetAmountValue;
        private int _currentAmount;

        public void Init(AchivementProgress data, int value)
        {
            _icon.sprite = data.icon;
            _achivementType = data.achivementType;
            _targetAmount.text = $"{value}/{data.targetAmount}";
            _name.text = data.name;
            _targetAmountValue = data.targetAmount;
            _currentAmount = value;
        }

        public bool CheckComplete()
        {
            return _currentAmount >= _targetAmountValue;
        }
    }
}