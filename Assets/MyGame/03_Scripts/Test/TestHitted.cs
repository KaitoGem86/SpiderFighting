using Animancer;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

public class TestHitted : MonoBehaviour, IHitted{
    public bool isPlayer;

    public void HittedByPlayer(FSMState state, float damage = 10)
    {
        Debug.Log("Hitted by player");
    }

    public void HittedBySpecialSkill(FSMState state, ClipTransitionSequence responseClip, float damage = 10)
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
    public bool IsPlayer { get => isPlayer;}
    public ClipTransitionSequence ResponseClip { get; set; }
}