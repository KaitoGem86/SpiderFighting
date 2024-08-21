using Core.GamePlay.Mission.NPC;
using UnityEngine;

namespace Core.GamePlay.Mission{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Protected Quest")]
    public class ProtectedQuest : Quest{
        public NPCSO npcSO;
        public Vector3 spawnPosition;

        public override void StartQuest()
        {
            base.StartQuest();
            npcSO.Spawn(spawnPosition);
        }
    }
}