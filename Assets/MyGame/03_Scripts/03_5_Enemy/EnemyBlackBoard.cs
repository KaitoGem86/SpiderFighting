using Animancer;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using UnityEngine;
using UnityEngine.AI;

namespace Core.GamePlay.Enemy
{
    public class EnemyBlackBoard : BlackBoard
    {
        [Header("========= General =========")]
        public EnemyController controller;
        public Vector3 defaultPosition;
        public Vector3 targetPosition;
        public AnimancerComponent animancerComponent;
        public EnemyModel[] enemyModels;
        public EnemyModel currentEnemyModel;


        [Header("========= Movement =========")]
        public NavMeshAgent navMeshAgent;
        public float elapsedTimeToChangeTarget = 1f;
        public bool isChasePlayer = false;

        [Header("========= Attack =========")]
        public float attackDelayTime = 5f;
        public bool isReadyToAttack = false;
        public IHitted targetToAttack;
        public Vector3 targetPos => targetToAttack.TargetEnemy.position;

        [Header("========= Weapon =========")]
        public EnemyWeaponController weaponController;
        public WeaponType weaponType;
        public DefaultEvent onReadyToAttack;
        public DefaultEvent onAttack;
        public DefaultEvent onCompleteAttack;

        void Awake()
        {
            defaultPosition = transform.position;
        }
    }
}