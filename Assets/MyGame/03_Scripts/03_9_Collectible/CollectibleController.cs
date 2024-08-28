using Core.GamePlay.MyPlayer;
using Core.UI.Popup;
using Data.Reward;
using MyTools.Event;
using UnityEngine;

namespace Collectible{
    public struct RuntimeRewardData{
        public int cash;
        public int exp;
    }

    public class CollectibleController : MonoBehaviour{
        public CollectibleData data;
        public float remainingTimeToSpawn;
        public CollectibleEvent onCollectEvent;
        public BoolEvent onCanCollect;
        private CollectibleSO _so;
        private int _id;
        
        public virtual void Init(CollectibleData data, CollectibleSO so, int id){
            this.data = data;
            _so = so;
            transform.position = data.position;
            _id = id;
        }

        public virtual void OnEnable(){
        }

        public virtual void OnDisable(){
            onCanCollect?.Raise(false);
        }

        public virtual void OnTriggerEnter(Collider other){
            if(other.CompareTag("Player")){
                other.GetComponent<PlayerBlackBoard>().CollectibleController = this;
                onCanCollect?.Raise(true);
            }
        }

        public void OnTriggerExit(Collider other){
            if(other.CompareTag("Player")){
                onCanCollect?.Raise(false);
            }
        }

        public virtual void OnCollect(){
            remainingTimeToSpawn = _so.timeToSpawn;
            onCollectEvent.Raise(data);
            this.gameObject.SetActive(false);
            onCanCollect?.Raise(false);
        }
    }
}