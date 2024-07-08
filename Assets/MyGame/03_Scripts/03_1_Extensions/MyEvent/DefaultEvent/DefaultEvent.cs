using UnityEngine;

namespace MyTools.Event{
    [CreateAssetMenu(fileName = nameof(DefaultEvent), menuName = ("GameEvents/" + nameof(DefaultEvent)), order = 0)]
    public class DefaultEvent : GameEvent{}
}