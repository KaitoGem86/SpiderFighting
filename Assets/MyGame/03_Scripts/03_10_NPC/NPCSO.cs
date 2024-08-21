using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Mission.NPC{
    [CreateAssetMenu(fileName = "New NPC", menuName = "NPC/NPCSO")]
    public class NPCSO : BaseSOWithPool{
        public int HP;

        public GameObject Spawn(Vector3 position){
            var go = SpawnObject();
            go.transform.position = position;
            return go;
        }
    }
}