using UnityEngine;

namespace Collectible{
    public class HPController : CollectibleController{
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            OnCollect();
        }
    }
}