using UnityEngine;

namespace MyTools.Event{
    [CreateAssetMenu(fileName = nameof(BoolEvent), menuName = ("GameEvents/" + nameof(BoolEvent)), order = 0)]
    public class BoolEvent : GameEvent<bool>{}
}