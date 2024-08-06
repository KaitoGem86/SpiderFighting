using Core.GamePlay.Support;
using UnityEngine;

public class TestHitted : MonoBehaviour, IHitted{
    public void HittedByPlayer()
    {
        Debug.Log("Hitted by player");
    }

    public Transform TargetEnemy
    {
        get => transform;
    }

    public bool IsIgnore { get; set; }
}