using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Enemy;
using UnityEngine;

namespace Core.GamePlay.Mission{
    public interface IMissionData{}

    [System.Serializable]
    public class FightingQuestData : IMissionData{
        public Vector3 position;
        public SerializedDictionary<EnemyType, int> count;
    }

    [CreateAssetMenu(fileName = "FightingQuestInitData", menuName = "Quest/FightingQuestInitData", order = 1)]
    public class FightingQuestInitData : QuestData<FightingQuestData>{
    }
}