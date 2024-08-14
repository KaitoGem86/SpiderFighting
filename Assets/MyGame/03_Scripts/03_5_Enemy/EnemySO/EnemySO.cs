using Core.GamePlay.Mission;
using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    [System.Serializable]
    public class EnemyData{
        public float HP = 100;
        public WeaponType enemyType;
        public EnemyData(EnemyData data){
            HP = data.HP;
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

        public GameObject Spawn(Vector3 position = default)
        {
            var go = SpawnObject();
            go.transform.position = position;
            go.GetComponent<EnemyController>().Init(this);
            go.transform.position = position;
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