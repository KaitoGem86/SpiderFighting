using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(menuName = "MyGame/ConfigElementSO")]
    public class ConfigElementSO : BaseSOWithPool{
        public override void Init(int poolId, Transform activeParent = null)
        {
            base.Init(poolId, activeParent);
        }

        public GameObject Spawn(StatData data)
        {
            var configElement = SpawnObject();
            configElement.GetComponent<ConfigElement>().Init(data);
            return configElement;
        }
    }
}