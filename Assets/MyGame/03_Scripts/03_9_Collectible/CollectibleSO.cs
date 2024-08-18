using System.Collections.Generic;
using Core.SystemGame.Factory;
using Data.Reward;
using UnityEngine;

namespace Collectible
{
    [System.Serializable]
    public struct CollectibleData
    {
        public RewardType rewardType;
        public int id;
        public int rewardValue;
        public Vector3 position;
    }

    [CreateAssetMenu(fileName = "New Collectible", menuName = "Collectible/New Collectible")]
    public class CollectibleSO : BaseSOWithPool
    {
        public float timeToSpawn;
        public List<CollectibleData> collectibleDatas;
        [HideInInspector] public List<CollectibleController> collectibles = new List<CollectibleController>();

        public void Init()
        {
            collectibles ??= new();
            collectibles.Clear();
            for (int i = 0; i < collectibleDatas.Count; i++)
            {
                collectibles.Add(Spawn(collectibleDatas[i], i));
            }
        }

        public CollectibleController Spawn(CollectibleData data, int id)
        {
            var collectible = SpawnObject().GetComponent<CollectibleController>();
            collectible.Init(data, this, id);
            return collectible;
        }
    }
}