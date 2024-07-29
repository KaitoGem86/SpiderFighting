using Unity.VisualScripting;
using UnityEngine;

namespace Core.GamePlay.Support
{

    public enum StatType
    {
        Health,
        ClimbingSpeed,
        JumpForce,
        MoveSpeed,
        StopSpeed,
        IncreaseSwingSpeed1,
        IncreaseSwingSpeed2,
        ZipSpeed,
        ZipDistanceClimb,
        ZipDistanceForward,
    }

    [CreateAssetMenu(menuName = "MyGame/StatData")]
    public class StatData : ScriptableObject
    {
        public StatType type;
        public float value;
        public float maxValue;
    }
}