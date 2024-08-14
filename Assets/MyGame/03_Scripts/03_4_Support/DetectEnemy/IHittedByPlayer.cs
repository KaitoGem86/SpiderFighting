using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Support{
    public interface IHitted
    {
        void HittedByPlayer(FSMState state);
        Transform TargetEnemy { get; }
        bool IsIgnore { get; set;}
        bool IsPlayer { get; }
    }
}