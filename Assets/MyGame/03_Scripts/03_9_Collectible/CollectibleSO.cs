using System.Collections.Generic;
using Core.SystemGame.Factory;
using Data.Reward;
using UnityEngine;

namespace Collectible
{
    [System.Serializable]
    public class CollectibleData
    {
        public RewardType rewardType;
        public int rewardValue;
        public Vector3 position;
    }

    [CreateAssetMenu(fileName = "New Collectible", menuName = "Collectible/New Collectible")]
    public class CollectibleSO : BaseSOWithPool
    {
        public List<CollectibleData> collectibleDatas;

        public void Init()
        {
            for (int i = 0; i < collectibleDatas.Count; i++)
            {
                Spawn(collectibleDatas[i]);
            }
        }

        public void Spawn(CollectibleData data)
        {
            var collectible = SpawnObject();
            collectible.GetComponent<CollectibleController>().Init(data);
        }
    }
}