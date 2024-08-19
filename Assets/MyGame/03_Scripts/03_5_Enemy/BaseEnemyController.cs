using System;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;
using DamageNumbersPro;
using Animancer;
using UnityEngine.AI;
using NodeCanvas.BehaviourTrees;

namespace Core.GamePlay.Enemy
{
    public class BaseEnemyController<T> : FSM<T>, IHitted where T : BaseEnemyBlackBoard
    {
        [SerializeField] protected RectTransform _unitCanvas;
        [SerializeField] protected HPBarController _hpBarController;
        [SerializeField] protected Transform _damgeNumberParent;
        [SerializeField] protected DamageNumber _damageNumber;
        [SerializeField] protected HitVFXController _hitVFXController;
        protected BaseEnemyData _initData;
        protected BaseEnemyData _runtimeData;

        public void OnDisable(){
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<BehaviourTreeOwner>().enabled = false;
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
                    _runtimeData.HP = Mathf.Clamp(_runtimeData.HP - 10, 0, _initData.HP);
                    _damageNumber.Spawn(_damgeNumberParent.position, 10, _damgeNumberParent);
                    _hpBarController.SetHP(_runtimeData.HP, _initData.HP);
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

        public void HittedBySpecialSkill(FSMState state, ClipTransitionSequence responseClip){

        }

        private void StunLock()
        {
            _runtimeData.HP = Mathf.Clamp(_runtimeData.HP - 10, 0, _initData.HP);
            _damageNumber.Spawn(_damgeNumberParent.position, 10, _damgeNumberParent);
            _hpBarController.SetHP(_runtimeData.HP, _initData.HP);
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
            _runtimeData.HP = Mathf.Clamp(_runtimeData.HP - 10, 0, _initData.HP);
            _damageNumber.Spawn(_damgeNumberParent.position, 10, _damgeNumberParent);
            _hpBarController.SetHP(_runtimeData.HP, _initData.HP);
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
        public bool IsPlayer { get => false; }
    }
}