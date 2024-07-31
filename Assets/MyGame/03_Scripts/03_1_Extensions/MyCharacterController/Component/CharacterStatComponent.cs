using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Support;
using MyTools.Event;
using MyTools.ScreenSystem;
using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController
{

    public class CharacterStatComponent<T1, T2> : BaseCharacterComponent<T1, T2> where T1 : MyCharacterController<T1> where T2 : CharacterBlackBoard<T1>
    {
        [SerializeField] private StatDatasController _statDatasController;
        [SerializeField] private string _statComponentId;
        protected Dictionary <StatType, StatData> _playerStats;
        
        public override void Init(T2 controller)
        {
            _playerStats = new Dictionary<StatType, StatData>();
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