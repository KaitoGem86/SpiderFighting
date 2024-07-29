using UnityEngine;

namespace Core.GamePlay.Support{
    public interface IHittedByPlayer
    {
        void HittedByPlayer();

        Transform TargetEnemy { get; }
    }
}