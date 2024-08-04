using UnityEngine;

namespace Core.GamePlay.Support{
    public interface IHitted
    {
        void HittedByPlayer();

        Transform TargetEnemy { get; }
        bool IsIgnore { get; set;}
    }
}