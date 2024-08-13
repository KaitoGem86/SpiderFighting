using Animancer;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using UnityEngine;
using UnityEngine.AI;

namespace Core.GamePlay.Enemy
{
    public class EnemyBlackBoard : BlackBoard
    {
        [Header("========= General =========")]
        public Vector3 defaultPosition;
        public Vector3 targetPosition;
        public AnimancerComponent animancerComponent;
        public EnemyModel[] enemyModels;
        public EnemyModel currentEnemyModel;


        [Header("========= Movement =========")]
        public NavMeshAgent navMeshAgent;
        public Transform target;
        public float elapsedTimeToChangeTarget = 1f;
        public bool isChasePlayer = false;

        [Header("========= Attack =========")]
        public float attackDelayTime = 5f;
        public Vector3 enemyPosition => target.position;
        public bool isReadyToAttack = false;
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