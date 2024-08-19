using Animancer;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

public class TestHitted : MonoBehaviour, IHitted{
    public void HittedByPlayer(FSMState state)
    {
        Debug.Log("Hitted by player");
    }

    public void HittedBySpecialSkill(FSMState state, ClipTransitionSequence responseClip)
    {
        Debug.Log("Hitted by special skill");
    }

    public void KnockBack()
    {
        Debug.Log("Knock back");
    }

    public Transform TargetEnemy
    {
        get => transform;
    }

    public bool IsIgnore { get; set; }
    public bool IsPlayer { get;}
}