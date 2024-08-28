using Core.GamePlay.Mission.NPC;
using UnityEngine;

namespace Core.GamePlay.Mission{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Protected Quest")]
    public class ProtectedQuest : Quest{
        public NPCSO npcSO;
        public Vector3 spawnPosition;
        private GameObject _npc;

        public override void StartQuest()
        {
            base.StartQuest();
            _npc = npcSO.Spawn(spawnPosition);
        }

        public override void FinishQuest(bool isWin = true, string questText = null)
        {
            base.FinishQuest(isWin, questText);
            npcSO.DespawnObject(_npc);
        }

        public override void ResetQuest()
        {
            base.ResetQuest();
            npcSO.DespawnObject(_npc);
        }
    }
}