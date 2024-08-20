using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Support{
    public interface IHitted
    {
        void HittedByPlayer(FSMState state);
        void HittedBySpecialSkill(FSMState state, ClipTransitionSequence responseClip);
        Transform TargetEnemy { get; }
        bool IsIgnore { get; set;}
        bool IsPlayer { get; }
        ClipTransitionSequence ResponseClip { get; set; }
    }
}