using UnityEngine;

namespace Core.GamePlay.Mission{
    public class QuestData<T> : ScriptableObject where T : IMissionData{
        public T data;
    }
}