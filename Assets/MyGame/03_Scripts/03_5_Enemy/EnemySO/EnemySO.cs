using Core.GamePlay.Mission;
using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class BaseEnemyData
    {
        public int Level;
        public float HP = 100;
        public float Damage;
        public float BlockRate;
        public float BlockTime;
        public float AttackRange;
        public float SightRange;
        public float Speed;
        public float CooldownAttackTime;
    }

    [System.Serializable]
    public class EnemyData : BaseEnemyData
    {
        public WeaponType enemyType;
        public EnemyData(EnemyData data)
        {
            HP = data.HP;
            Damage = data.Damage;
            BlockRate = data.BlockRate;
            BlockTime = data.BlockTime;
            AttackRange = data.AttackRange;
            SightRange = data.SightRange;
            Speed = data.Speed;
            CooldownAttackTime = data.CooldownAttackTime;
            enemyType = data.enemyType;
        }
    }

    public enum EnemyType
    {
        UnArm,
        MeleeWeapon,
        Gun,
        Boss
    }

    [CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/EnemySO", order = 1)]
    public class EnemySO : BaseSOWithPool
    {
        public EnemyData initData;

        public virtual GameObject Spawn(Transform parent = null)
        {
            var go = SpawnObject();
            go.transform.SetParent(parent);
            go.transform.localPosition = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
            go.GetComponent<EnemyController>().Init(this);
            return go;
        }

        // public GameObject Spawn(Vector3 position){
        //     var go = SpawnObject();
        //     go.GetComponent<EnemyController>().Init(this);
        //     go.transform.position = position;
        //     return go;
        // }
    }
}