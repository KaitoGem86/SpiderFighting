using UnityEngine;

namespace MyTools.Event{
    [CreateAssetMenu(fileName = nameof(FloatEvent), menuName = ("GameEvents/" + nameof(FloatEvent)), order = 0)]
    public class FloatEvent : GameEvent<float>{}
}