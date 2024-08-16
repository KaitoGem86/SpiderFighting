using System.Collections.Generic;
using Core.SystemGame.Factory;
using Data.Reward;
using UnityEngine;

namespace Collectible
{
    public class CollectibleData
    {
        public RewardType rewardType;
        public int rewardValue;
        public Vector3 position;
    }

    public class CollectibleSO : BaseSOWithPool
    {
        public List<CollectibleData> collectibleDatas;

        public void Spawn(CollectibleData data)
        {
            var collectible = SpawnObject();
            collectible.GetComponent<CollectibleController>().Init(data);
        }
    }
}