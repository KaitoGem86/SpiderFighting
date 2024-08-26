using System.Collections.Generic;
using Core.GamePlay.Mission.NPC;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class ShippingQuestStep : QuestStep<ShippingQuestInitData>
    {
        [SerializeField] private WaitFoodNPCSO _npcSO;
        List<GameObject> _npcs = new List<GameObject>();

        public override void Init(Quest container)
        {
            base.Init(container);
            foreach (var pos in _questData.positions)
            {
                var go = _npcSO.Spawn(pos);
                _npcs.Add(go);
            }
        }

        public override void ResetStep()
        {
            base.ResetStep();
            foreach (var npc in _npcs)
            {
                _npcSO.DespawnObject(npc);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FinishStep();
            }
        }
    }
}