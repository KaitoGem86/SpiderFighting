using UnityEngine;
using NodeCanvas.BehaviourTrees;
using UnityEngine.AI;
using MyTools.Event;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Enemy{
    public class BossController : BaseEnemyController<BossBlackBoard>{
        private BossSO _bossSO;
        [SerializeField] private DefaultEvent _onBossActive;
        

        public void Init(BossSO bossSO){
            _initData = bossSO.bossData;
            _bossSO = bossSO;
            _runtimeData = new BossData(bossSO.bossData);
            IsIgnore = false;
            blackBoard.weaponController.SetTypeOfEnemy(WeaponType.Hand, blackBoard.bossModel.rightHand);
            _hpBarController.SetHP(_runtimeData.HP, bossSO.bossData.HP);
            blackBoard.defaultPosition = transform.position;
            ChangeAction(_startState);
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<BehaviourTreeOwner>().enabled = true;
            _onBossActive.Raise();
            blackBoard.onBossHPChange.Raise(1);
        }

        public override void HittedByPlayer(FSMState state)
        {
            base.HittedByPlayer(state);
            blackBoard.onBossHPChange.Raise(_runtimeData.HP / _initData.HP);
        }
    }
}