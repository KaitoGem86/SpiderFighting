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
        public CustomEvent.DisplayInfo.DisplayInfo info;

        public override GameObject Spawn(Transform parent)
        {
            var go = SpawnObject();
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<BossController>().Init(this);
            return go;
        }
    }
}