using UnityEngine;
using NodeCanvas.BehaviourTrees;
using UnityEngine.AI;
using MyTools.Event;
using Extensions.SystemGame.AIFSM;
using CustomEvent.DisplayInfo;

namespace Core.GamePlay.Enemy
{
    public class BossController : BaseEnemyController<BossBlackBoard>
    {
        private BossSO _bossSO;
        [SerializeField] private DisplayInfoEvent _onBossActive;
        [SerializeField] private bool _isSpawnInMission = true;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_isSpawnInMission)
            {
                Init(blackBoard.bossSO);
            }
        }

        public void Init(BossSO bossSO)
        {
            _initData = bossSO.bossData;
            _bossSO = bossSO;
            _runtimeData = new BossData(bossSO.bossData);
            IsIgnore = false;
            blackBoard.weaponController.SetTypeOfEnemy(WeaponType.Hand, blackBoard.bossModel.rightHand);
            blackBoard.defaultPosition = transform.position;
            ChangeAction(_startState);
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<BehaviourTreeOwner>().enabled = true;
            _onBossActive.Raise(_bossSO.info);
            blackBoard.onBossHPChange.Raise(1);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            blackBoard.onBossHPChange.Raise(-1);
        }

        public override void HittedByPlayer(FSMState state, float damage = 10)
        {
            if (_currentState.StateType == FSMState.Attack || _currentState.StateType == FSMState.UltimateAttack)
            {
                _hitVFXController.ShowHitVFX();
                _runtimeData.HP = Mathf.Clamp(_runtimeData.HP - 10, 0, _initData.HP);
                _damageNumber.Spawn(_damgeNumberParent.position, 10, _damgeNumberParent);
                _hpBarController?.SetHP(_runtimeData.HP, _initData.HP);
                blackBoard.attackDelayTime = 5;
                if (_runtimeData.HP <= 0)
                {
                    IsIgnore = true;
                    ChangeAction(FSMState.Dead);
                }
                blackBoard.onBossHPChange.Raise(_runtimeData.HP / _initData.HP);
                return;
            }
            base.HittedByPlayer(state);
            blackBoard.onBossHPChange.Raise(_runtimeData.HP / _initData.HP);
        }
    }
}