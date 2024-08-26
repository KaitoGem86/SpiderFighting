using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay.Mission{
    [CreateAssetMenu(fileName = "ShippingQuestInitData", menuName = "Quest/ShippingQuestInitData", order = 1)]
    public class ShippingQuestInitData : ScriptableObject, IMissionData{    
        public List<Vector3> positions;
    }
}