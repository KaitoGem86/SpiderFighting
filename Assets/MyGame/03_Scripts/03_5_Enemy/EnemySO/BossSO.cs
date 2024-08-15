using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    [System.Serializable]
    public class BossData : BaseEnemyData{
        public BossData(BossData data){
            HP = data.HP;
            Damage = data.Damage;
            BlockRate = data.BlockRate;
            BlockTime = data.BlockTime;
            AttackRange = data.AttackRange;
            SightRange = data.SightRange;
            Speed = data.Speed;
            CooldownAttackTime = data.CooldownAttackTime;
        }
    }

    [CreateAssetMenu(fileName = "BossSO", menuName = "MyGame/Enemy/BossSO", order = 1)]
    public class BossSO : EnemySO
    {   
        public BossData bossData;

        public override GameObject Spawn(Vector3 position = default)
        {
            var go = SpawnObject();
            go.transform.position = position;
            go.GetComponent<BossController>().Init(this);
            go.transform.position = position;
            return go;
        }
    }
}