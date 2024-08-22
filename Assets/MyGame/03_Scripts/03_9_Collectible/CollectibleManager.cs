using System.Collections.Generic;
using Data.Reward;
using UnityEditor;
using UnityEngine;

namespace Collectible{
    public class CollectibleManager : MonoBehaviour{
        public CollectibleSO chest;
        public CollectibleSO hp;
        public List<CollectibleController> waitToActive = new List<CollectibleController>();

        public void Awake(){
            chest.Init();
            hp.Init();
        }

        private void Update(){
            for(int i = waitToActive.Count - 1; i >= 0; i--){
                waitToActive[i].remainingTimeToSpawn -= Time.deltaTime;
                if(waitToActive[i].remainingTimeToSpawn <= 0){
                    waitToActive[i].gameObject.SetActive(true);
                    waitToActive.RemoveAt(i);
                }
            }
        }

        public void OnCollect(CollectibleData data){
            var collectible = GetValidSO(data.rewardType).collectibles[data.id];
            waitToActive.Add(collectible);
        }

        private CollectibleSO GetValidSO(RewardType rewardType){
            return rewardType switch
            {
                RewardType.HP => hp,
                _ => chest
            };
        }



#if UNITY_EDITOR
        [SerializeField] private Transform _chestParent;
        [SerializeField] private Transform _hpParent;

        [ContextMenu("Load Collectible data")]
        public void Load(){
            LoadCollectible(_chestParent, chest);
            LoadCollectible(_hpParent, hp);
        }

        private void LoadCollectible(Transform parent, CollectibleSO collectible){
            collectible.collectibleDatas.Clear();
            var type = GetRewardType(parent);
            for (int i = 0; i < parent.childCount; i++){
                var child = parent.GetChild(i);
                var data = new CollectibleData
                {
                    rewardType = type,
                    position = child.position,
                    rewardValue = 10,
                    id = i,
                };
                collectible.collectibleDatas.Add(data);
            }
            EditorUtility.SetDirty(collectible);
        }

        private RewardType GetRewardType(Transform parent){
            if(parent.name.Contains("Chest")){
                return RewardType.EXP;
            }
            if(parent.name.Contains("HP")){
                return RewardType.HP;
            }
            return RewardType.HP;
        }
#endif
    }
}