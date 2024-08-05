using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay.Mission{
    public interface IMissionData{}
    
    [CreateAssetMenu(fileName = "FightingQuestInitData", menuName = "Quest/FightingQuestInitData", order = 1)]
    public class FightingQuestInitData : ScriptableObject, IMissionData{
        public Vector3 position;
        public List<EnemyData> data;
    }
}