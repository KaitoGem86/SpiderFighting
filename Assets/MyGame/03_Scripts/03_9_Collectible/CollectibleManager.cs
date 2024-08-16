using UnityEditor;
using UnityEngine;

namespace Collectible{
    public class CollectibleManager : MonoBehaviour{
        public CollectibleSO chest;
        public CollectibleSO hp;

        public void Awake(){
            chest.Init();
            hp.Init();
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
            for (int i = 0; i < parent.childCount; i++){
                var child = parent.GetChild(i);
                var data = new CollectibleData
                {
                    position = child.position,
                    rewardValue = 10
                };
                collectible.collectibleDatas.Add(data);
            }
            EditorUtility.SetDirty(collectible);
        }
#endif
    }
}