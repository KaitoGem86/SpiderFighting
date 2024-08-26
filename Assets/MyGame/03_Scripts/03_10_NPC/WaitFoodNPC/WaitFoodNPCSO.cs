using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Mission.NPC{
    [CreateAssetMenu(fileName = "WaitFoodNPCSO", menuName = "NPC/WaitFoodNPCSO")]
    public class WaitFoodNPCSO : BaseSOWithPool{
        public GameObject Spawn(Vector3 position){
            var go = SpawnObject();
            go.GetComponent<WaitFoodNPC>().Init(this);
            go.transform.position = position;
            return go;
        }
    }
}