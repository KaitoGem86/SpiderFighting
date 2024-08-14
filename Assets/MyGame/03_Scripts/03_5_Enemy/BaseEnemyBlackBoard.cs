using Animancer;
using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;
using UnityEngine.AI;

namespace Core.GamePlay.Enemy
{
    public class BaseEnemyBlackBoard : BlackBoard
    {
        [Header("========= General =========")]
        public EnemyController controller;
        public Vector3 defaultPosition;
        public Vector3 targetPosition;
        public AnimancerComponent animancerComponent;

        [Header("========= Movement =========")]
        public NavMeshAgent navMeshAgent;
        public float speed = 5f;

        [Header("========= Attack =========")]
        public float damage;
        public float attackDelayTime = 5f;
        public bool isReadyToAttack = false;
        public float sightRange = 10f;
        public float attackRange = 2f;
        public IHitted targetToAttack;
        public Vector3 targetPos => targetToAttack.TargetEnemy.position;

        [Header("========= Weapon =========")]
        public EnemyWeaponController weaponController;
        public WeaponType weaponType;
    }
}