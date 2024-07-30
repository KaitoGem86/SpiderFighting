using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Support;
using MyTools.Event;
using MyTools.ScreenSystem;
using UnityEngine;

namespace Extentions.SystemGame.MyCharacterController
{

    public class CharacterStatComponent : BaseCharacterComponent
    {
        [SerializeField] private StatDatasController _statDatasController;
        [SerializeField] SerializedDictionary<StatType, StatData> _playerStats;
        [SerializeField] private string _statComponentId;

        public override void Init(MyCharacterController controller)
        {
            _playerStats = new SerializedDictionary<StatType, StatData>();
            foreach (var statData in _statDatasController.StatDatas)
            {
                _playerStats.Add(statData.type, statData);
            }
        }

        public void UpdateStat(StatType statType, float value)
        {
            if (_playerStats.ContainsKey(statType))
            {
                _playerStats[statType].value += value;
            }
        }

        public StatData GetValue(StatType statType)
        {
            if (_playerStats.ContainsKey(statType))
            {
                return _playerStats[statType];
            }
            else
            {
                throw new Exception("Character does not have this stat type");
            }
        }
    }
}