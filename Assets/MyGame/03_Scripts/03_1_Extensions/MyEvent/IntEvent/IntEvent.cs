using UnityEngine;

namespace MyTools.Event{
    [CreateAssetMenu(fileName = nameof(IntEvent), menuName = ("GameEvents/" + nameof(IntEvent)), order = 0)]
    public class IntEvent : GameEvent<int>{}
}