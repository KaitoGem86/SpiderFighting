using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class GadgetsController : MonoBehaviour
    {
        [SerializeField] private BaseGadgetEquip[] _gadgets;
        private BaseGadgetEquip _currentGadget;

        public void UseGadget()
        { 
            _currentGadget.UseGadget();
        }

        public void ChangeGadget(int index) {
            if (_currentGadget != null)
            {
                _currentGadget.gameObject.SetActive(false);
            }

            _currentGadget = _gadgets[index];
            _currentGadget.gameObject.SetActive(true);
        }
    }
}