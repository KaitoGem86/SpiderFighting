using System;
using Core.GamePlay.Support;
using DG.Tweening.Core.Easing;
using Extensions.SystemGame.AIFSM;
using NodeCanvas.BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

namespace Core.GamePlay.Enemy
{


    public class EnemyController : BaseEnemyController<EnemyBlackBoard>
    {
        [SerializeField] private EnemyGroupSO _enemyGroupSO;
        private EnemySO _soController;

        protected override void OnEnable()
        {
            base.OnEnable();
            _enemyGroupSO.AddEnemy(this);
            blackBoard.isReadyToAttack = false;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            blackBoard.onEnemyDead?.Invoke();
            blackBoard.onEnemyDead = null;
            _enemyGroupSO.RemoveEnemy(this);
        }

        public void Init(EnemySO soConTroller)
        {
            _soController = soConTroller;
            _initData = soConTroller.initData;
            _runtimeData = new EnemyData(_soController.initData);
            blackBoard.Init(runtimeData);
            RandomEnemySkin();
            SetEnemyType(soConTroller.initData.enemyType);
            IsIgnore = false;
            _hpBarController.SetHP(_runtimeData.HP, _soController.initData.HP);
            blackBoard.defaultPosition = transform.position;
            ChangeAction(_startState);
            
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<BehaviourTreeOwner>().enabled = true;
        }

        private void SetEnemyType(WeaponType type)
        {
            blackBoard.weaponController.SetTypeOfEnemy(type, blackBoard.currentEnemyModel.rightHand);
            blackBoard.weaponType = type;
        }

        private void RandomEnemySkin()
        {
            foreach (var model in blackBoard.enemyModels)
            {
                model.gameObject.SetActive(false);
            }
            var index = UnityEngine.Random.Range(0, blackBoard.enemyModels.Length);
            blackBoard.currentEnemyModel = blackBoard.enemyModels[index];
            blackBoard.currentEnemyModel.gameObject.SetActive(true);
            blackBoard.animancerComponent.Animator.avatar = blackBoard.currentEnemyModel.animator.avatar;
        }

        public void EnemyDead()
        {
            _soController.DespawnObject(this.gameObject);
        }

        private EnemyData runtimeData => (EnemyData)_runtimeData;
        public bool CanAttackInGroup => _enemyGroupSO.CheckAttack;
        public EnemySO EnemySO { get => _soController; }
    }
}