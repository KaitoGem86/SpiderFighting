using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class BaseGadgetEquip : MonoBehaviour{
        [SerializeField] protected bool _isActive = true;

        public virtual void UseGadget(){
        }
    }
}