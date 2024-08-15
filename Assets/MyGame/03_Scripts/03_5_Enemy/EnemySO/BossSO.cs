using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class BossData : BaseEnemyData{

    }

    public class BossSO : BaseSOWithPool
    {   
        public BossData initData;

        public GameObject Spawn(Vector3 position = default)
        {
            var go = SpawnObject();
            go.transform.position = position;
            go.GetComponent<BossController>().Init(this);
            go.transform.position = position;
            return go;
        }
    }
}