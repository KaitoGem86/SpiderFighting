using Data.Reward;
using MyTools.Event;
using UnityEngine;

namespace Collectible{
    [CreateAssetMenu(fileName = "CollectibleEvent", menuName = "GameEvents/CollectibleEvent")]
    public class CollectibleEvent : GameEvent<CollectibleData>{}
}