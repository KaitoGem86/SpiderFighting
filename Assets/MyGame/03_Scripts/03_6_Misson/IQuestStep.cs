using UnityEngine;

namespace Core.GamePlay.Mission{
    public interface IQuestStep{
        void Init(Quest container);
    
        public GameObject GameObject { get; }
    }
}