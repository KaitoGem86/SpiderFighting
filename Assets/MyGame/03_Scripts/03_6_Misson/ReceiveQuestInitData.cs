using UnityEngine;

namespace Core.GamePlay.Mission
{
    [CreateAssetMenu(fileName = "ShippingQuestInitData", menuName = "Quest/ShippingQuestInitData", order = 1)]

    public class ReceiveQuestInitData : ScriptableObject, IMissionData
    {
        public Vector3 position;
    }
}