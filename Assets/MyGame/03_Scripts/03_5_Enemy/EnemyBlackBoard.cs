using Animancer;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using UnityEngine;
using UnityEngine.AI;

namespace Core.GamePlay.Enemy
{
    public class EnemyBlackBoard : BaseEnemyBlackBoard
    {
        public override IFSM fsm { get => enemyController;}
        public override IHitted hitted {get => enemyController;}

        [Header("========= General =========")]
        public EnemyController enemyController;
        public EnemyModel[] enemyModels;
        public EnemyModel currentEnemyModel;

        public DefaultEvent onAttack;
        public DefaultEvent onCompleteAttack;

        void Awake()
        {
            //defaultPosition = transform.position;
        }

        public void Init(EnemyData data){
            attackRange = data.AttackRange;
            sightRange = data.SightRange;
            speed = data.Speed;
            attackDelayTime = data.CooldownAttackTime;
            weaponType = data.enemyType;
        }
    }
}