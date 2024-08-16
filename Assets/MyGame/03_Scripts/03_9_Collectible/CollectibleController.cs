using Data.Reward;
using UnityEngine;

namespace Collectible{
    public class CollectibleController : MonoBehaviour{
        public CollectibleData data;
        
        public void Init(CollectibleData data){
            this.data = data;
            transform.position = data.position;
        }
    }
}