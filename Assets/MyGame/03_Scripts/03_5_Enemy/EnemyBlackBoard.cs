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
        [Header("========= General =========")]
        public EnemyModel[] enemyModels;
        public EnemyModel currentEnemyModel;

        [Header("========= Weapon =========")]
        public EnemyWeaponController weaponController;
        public WeaponType weaponType;

        void Awake()
        {
            defaultPosition = transform.position;
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