using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.SystemGame.Pooling{
    public class SimplePool {
        private SimplePool(){
            _dictPool = new Dictionary<GameObject, Queue<GameObject>>();
            _poolParent = new GameObject("PoolParent");
        }

        private static SimplePool _instance;

        public static SimplePool Instance {
            get{
                if(_instance == null){
                    _instance = new SimplePool();
                }
                return _instance;
            }
        }

        private GameObject _poolParent;
        private Dictionary<GameObject, Queue<GameObject>> _dictPool;

        private void CreatePool(GameObject prefab, int count){
            if(!_dictPool.ContainsKey(prefab)){
                _dictPool.Add(prefab, new Queue<GameObject>());
            }

            for(int i = 0; i < count; i++){
                GameObject obj = Object.Instantiate(prefab);
                PoolMember tmp = obj.AddComponent<PoolMember>();
                tmp.Init(_dictPool.Count - 1);
                obj.SetActive(false);
                obj.transform.SetParent(_poolParent.transform);
                _dictPool[prefab].Enqueue(obj);
            }
        }

        public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation){
            if(!_dictPool.ContainsKey(prefab)){
                CreatePool(prefab, 1);
            }
            if(_dictPool[prefab].Count == 0){
                CreatePool(prefab, 1);
            }
            GameObject obj = _dictPool[prefab].Dequeue();
            while(obj.GetComponent<PoolMember>().State == MemberStateEnum.InUse){
                obj = _dictPool[prefab].Dequeue();
            }
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }

        public GameObject SpawnObject(GameObject prefab, Vector3 position){
            return SpawnObject(prefab, position, Quaternion.identity);
        }

        public GameObject SpawnObject(GameObject prefab){
            return SpawnObject(prefab, Vector3.zero, Quaternion.identity);
        }

        public GameObject SpawnObject(GameObject prefab, Transform parent){
            GameObject obj = SpawnObject(prefab);
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero;
            return obj;
        }

        public void ReturnObject(GameObject obj){
            int poolId = obj.GetComponent<PoolMember>().MemberId;
            obj.SetActive(false);
            obj.transform.SetParent(_poolParent.transform);
            _dictPool.ElementAt(poolId).Value.Enqueue(obj);
        }
    }
}