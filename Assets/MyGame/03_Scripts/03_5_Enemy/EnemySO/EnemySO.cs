using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/EnemySO", order = 1)]
    public class EnemySO : BaseSOWithPool, IFactoryItem
    {
        public GameObject Spawn(Vector3 position = default)
        {
            var go = SpawnObject();
            go.transform.position = position;
            go.GetComponent<EnemyController>().Init(this);
            go.transform.position = position;
            return go;
        }
    }
}