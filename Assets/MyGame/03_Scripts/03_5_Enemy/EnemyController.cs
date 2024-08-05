using System;
using Core.GamePlay.Support;
using DG.Tweening.Core.Easing;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    

    public class EnemyController : AIFSM, IHitted
    {
        [SerializeField] private RectTransform _unitCanvas;
        [SerializeField] private HPBarController _hpBarController;
        [SerializeField] FloatingDamageTextSO _floatingDamageTextSO;
        private EnemySO _soController;
        private EnemyData _runtimeData;

        public void OnDisable(){
            onEnemyDead?.Invoke();
            onEnemyDead = null;
        }

        public void Init(EnemySO soConTroller)
        {
            _soController = soConTroller;
            _runtimeData = new EnemyData(_soController.initData);
            IsIgnore = false;
            _hpBarController.SetHP(_runtimeData.HP, _soController.initData.HP);
        }

        public void HittedByPlayer()
        {
            _runtimeData.HP = Mathf.Clamp(_runtimeData.HP - 10, 0, _soController.initData.HP);
            _floatingDamageTextSO.Spawn(10, _unitCanvas);
            _hpBarController.SetHP(_runtimeData.HP, _soController.initData.HP);
            if (_runtimeData.HP <= 0)
            {
                IsIgnore = true;
                ChangeAction(AIState.Dead);
            }
            else
            {
                ChangeAction(AIState.Hit);
            }
        }

        public Transform TargetEnemy { get => this.transform; }
        public bool IsIgnore { get; set; }
        public EnemySO EnemySO { get => _soController; }
        public Action onEnemyDead;
    }
}