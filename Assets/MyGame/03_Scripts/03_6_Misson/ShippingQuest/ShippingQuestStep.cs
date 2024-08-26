using System.Collections.Generic;
using Core.GamePlay.Mission.NPC;
using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class ShippingQuestStep : QuestStep<ShippingQuestInitData>
    {
        [SerializeField] private WaitFoodNPCSO _npcSO;
        [SerializeField] private IntEvent _onCompleteAnShipping;
        List<GameObject> _npcs = new List<GameObject>();

        private int _currentNPCIndex = 0;

        public override void Init(Quest container)
        {
            base.Init(container);
            _currentNPCIndex = 0;
            _npcs.Clear();
            foreach (var pos in _questData.positions)
            {
                var go = _npcSO.Spawn(pos, this);
                _npcs.Add(go);
            }
        }

        public override void FinishStep()
        {
            foreach (var npc in _npcs)
            {
                _npcSO.DespawnObject(npc);
            }
            _npcs.Clear();
            base.FinishStep();
        }

        public override void ResetStep()
        {
            base.ResetStep();
            foreach (var npc in _npcs)
            {
                _npcSO.DespawnObject(npc);
            }
        }

        public void OnCompleteAnShipping(){
            _currentNPCIndex++;
            _onCompleteAnShipping.Raise(_currentNPCIndex - 1);
            if (_currentNPCIndex >= _npcs.Count)
            {
                _container.NextQuestStep();
            }
        }
    }
}