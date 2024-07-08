using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using MyTools.Event;
using MyTools.ScreenSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    public enum PlayerStatType
    {
        Health,
        Speed,
        
    }

    [Serializable]
    public class PlayerStat
    {
        public float value;
        public float maxValue;

        public void Init()
        {
            this.value = maxValue;
        }
    }

    public class PlayerStatComponent : BasePlayerComponent
    {
        [SerializeField] SerializedDictionary<PlayerStatType, PlayerStat> _playerStats;
        [SerializeField] SerializedDictionary<PlayerStatType, FloatEvent> _onPlayerStatUpdate;
        [SerializeField] private string _statComponentId;

        private bool _isInit = false;
        private bool _isDie = false;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
        {
            StartCoroutine(UpdatePlayerStatAfterAwake());
        }

        public override void Init(PlayerController playerController)
        {
            base.Init(playerController);
            foreach (var stat in _playerStats)
            {
                Debug.Log($"PlayerStatComponent.Init: {stat.Key} {stat.Value.value} {stat.Value.maxValue}");
                //_playerStats[stat.Key].Init();
                _onPlayerStatUpdate[stat.Key]?.Raise(stat.Value.value / stat.Value.maxValue);
            }
        }

        void OnApplicationQuit()
        {
        }

        public void OnUpdatePlayerRuntimeValue(PlayerStatType statType, float value, bool isPercent = false)
        {
            if(!_isInit) return;
            if(CheckLoseGame()){
                if(_isDie) return;
                _isDie = true;
                _playerController.ResolveComponent<PlayerStateComponent>(PlayerComponentEnum.State).ChangeAction(ActionEnum.Die);
                StartCoroutine(ActiveLoseGamePopupCoroutine());
                return;
            }
            if (isPercent)
            {
                _playerStats[statType].value += _playerStats[statType].maxValue * value;
                _playerStats[statType].value = Mathf.Clamp(_playerStats[statType].value, 0, _playerStats[statType].maxValue);
            }
            else
            {
                _playerStats[statType].value += value;
                _playerStats[statType].value = Mathf.Clamp(_playerStats[statType].value, 0, _playerStats[statType].maxValue);
            }
            _onPlayerStatUpdate[statType]?.Raise(_playerStats[statType].value  / _playerStats[statType].maxValue);
            
        }

        // public void OnUseFood(ItemInfo itemInfo)
        // {
        //     if(!_isInit) return;
        //     var data = _inventory.InventorySO.DictItemDetails[itemInfo.itemType][itemInfo.id];
        //     if(data == null) return;
        //     if(data.isMaterials) throw new Exception("PlayerStatComponent.OnUseFood: This item is not food. How can use a materials as food?. Please check again");
        //     foreach (var stat in data.dictIncreaseStat)
        //     {
        //         OnUpdatePlayerRuntimeValue(stat.Key, stat.Value);
        //     }

        // }

        private IEnumerator UpdatePlayerStatAfterAwake(){
            yield return new WaitForSeconds(0.1f);
            foreach (var stat in _playerStats)
            {
                _onPlayerStatUpdate[stat.Key]?.Raise(stat.Value.value / stat.Value.maxValue);
            }
            _isInit = true;
        }

        private IEnumerator ActiveLoseGamePopupCoroutine(){
            yield return new WaitForSeconds(3f);
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.LoseGame);
        }

        private bool CheckLoseGame(){
            if(_playerStats[PlayerStatType.Health].value <= 0){
                return true;
            }
            return false;
        }
    }
}