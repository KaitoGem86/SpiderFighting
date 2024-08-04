using System.Collections.Generic;
using System.Numerics;
using Core.GamePlay.Enemy;

namespace Core.GamePlay.Mission{
    public interface IMissionData{}

    [System.Serializable]
    public class FightingQuestData : IMissionData{
        public Vector3 position;
        public Dictionary<EnemyType, int> count;
    }
}