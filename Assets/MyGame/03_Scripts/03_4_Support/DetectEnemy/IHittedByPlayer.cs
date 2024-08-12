using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Support{
    public interface IHitted
    {
        void HittedByPlayer(FSMState state);
        void KnockBack();
        Transform TargetEnemy { get; }
        bool IsIgnore { get; set;}
    }
}