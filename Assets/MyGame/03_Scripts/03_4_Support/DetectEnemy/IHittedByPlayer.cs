using UnityEngine;

namespace Core.GamePlay.Support{
    public interface IHitted
    {
        void HittedByPlayer();
        void KnockBack();
        Transform TargetEnemy { get; }
        bool IsIgnore { get; set;}
    }
}