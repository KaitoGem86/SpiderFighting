using MyTools.Event;
using UnityEngine;

namespace CustomEvent.DisplayInfo{
    [System.Serializable]
    public struct DisplayInfo{
        public Sprite icon;
        public string name;
    }

    [CreateAssetMenu(fileName = "DisplayInfoEvent", menuName = "GameEvents/DisplayInfoEvent")]
    public class DisplayInfoEvent : GameEvent<DisplayInfo>{}
}