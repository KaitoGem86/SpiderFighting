using System;
using Core.GamePlay.Support;
using DG.Tweening.Core.Easing;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{


    public class EnemyController : FSM<EnemyBlackBoard>, IHitted
    {
        [SerializeField] private RectTransform _unitCanvas;
        [SerializeField] private HPBarController _hpBarController;
        [SerializeField] FloatingDamageTextSO _floatingDamageTextSO;
        [SerializeField] private HitVFXController _hitVFXController;
        private EnemySO _soController;
        private EnemyData _runtimeData;

        protected override void OnEnable()
        {
            base.OnEnable();
            blackBoard.isReadyToAttack = false;
        }

        public void OnDisable()
        {
            onEnemyDead?.Invoke();
            onEnemyDead = null;
            blackBoard.onAttack?.Raise();
            blackBoard.onCompleteAttack?.Raise();
        }

        public void Init(EnemySO soConTroller)
        {
            _soController = soConTroller;
            _runtimeData = new EnemyData(_soController.initData);
            IsIgnore = false;
            _hpBarController.SetHP(_runtimeData.HP, _soController.initData.HP);
        }

        public void HittedByPlayer(FSMState state)
        {
            switch (state)
            {
                case FSMState.StunLock:
                    StunLock();
                    break;
                case FSMState.KnockBack:
                    KnockBack();
                    break;
                default:
                    _hitVFXController.ShowHitVFX();
                    _runtimeData.HP = Mathf.Clamp(_runtimeData.HP - 10, 0, _soController.initData.HP);
                    _floatingDamageTextSO.Spawn(10, _unitCanvas);
                    _hpBarController.SetHP(_runtimeData.HP, _soController.initData.HP);
                    blackBoard.attackDelayTime = 5;
                    if (_runtimeData.HP <= 0)
                    {
                        IsIgnore = true;
                        ChangeAction(FSMState.Dead);
                    }
                    else
                    {
                        ChangeAction(FSMState.Hit);
                    }
                    break;
            }
        }

        private void StunLock()
        {
            _runtimeData.HP = Mathf.Clamp(_runtimeData.HP - 10, 0, _soController.initData.HP);
            _floatingDamageTextSO.Spawn(10, _unitCanvas);
            _hpBarController.SetHP(_runtimeData.HP, _soController.initData.HP);
            blackBoard.attackDelayTime = 5;
            if (_runtimeData.HP <= 0)
            {
                IsIgnore = true;
                ChangeAction(FSMState.Dead);
            }
            else
            {
                ChangeAction(FSMState.StunLock);
            }
        }

        public void KnockBack()
        {
            _hitVFXController.ShowHitVFX();
            _runtimeData.HP = Mathf.Clamp(_runtimeData.HP - 10, 0, _soController.initData.HP);
            _floatingDamageTextSO.Spawn(10, _unitCanvas);
            _hpBarController.SetHP(_runtimeData.HP, _soController.initData.HP);
            blackBoard.attackDelayTime = 5;
            if (_runtimeData.HP <= 0)
            {
                IsIgnore = true;
                ChangeAction(FSMState.Dead);
            }
            else
            {
                ChangeAction(FSMState.KnockBack);
            }
        }

        public Transform TargetEnemy { get => this.transform; }
        public bool IsIgnore { get; set; }
        public EnemySO EnemySO { get => _soController; }
        public Action onEnemyDead;
    }
}