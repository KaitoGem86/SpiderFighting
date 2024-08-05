using UnityEngine;
using System.Collections.Generic;
using Core.SystemGame.Pooling;

namespace Core.SystemGame.Factory
{
    public class BaseSOWithPool : ScriptableObject
    {
        [SerializeField] protected GameObject _prefab;
        protected Queue<GameObject> _pool = new Queue<GameObject>();
        protected Transform _poolParent;
        protected Transform _activeParent;
        protected int _poolId;
        protected bool _isInit = false;

        public virtual void Init(int poolId, Transform activeParent = null)
        {
            if (_isInit)
                return;
            _isInit = true;
            _poolId = poolId;
            _activeParent = activeParent;
            _poolParent = new GameObject("Pool " + name).transform;
        }


        private void CreatePool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(_prefab);
                obj.SetActive(false);
                obj.AddComponent<PoolMember>().Init(_poolId);
                obj.transform.SetParent(_poolParent);
                _pool.Enqueue(obj);
            }
        }

        protected GameObject SpawnObject()
        {
            if (_pool.Count == 0)
            {
                CreatePool(1);
            }
            GameObject obj = _pool.Dequeue();
            while (obj.GetComponent<PoolMember>().State == MemberStateEnum.InUse)
            {
                if(_pool.Count == 0)
                    CreatePool(1);
                obj = _pool.Dequeue();
            }
            obj.GetComponent<PoolMember>().Init(_poolId);
            obj.transform.SetParent(_activeParent);
            obj.SetActive(true);
            return obj;
        }

        public void DespawnObject(GameObject obj)
        {
            if (obj.GetComponent<PoolMember>() == null || obj.GetComponent<PoolMember>().MemberId != _poolId)
            {
                Debug.LogError("Wrong pool " + obj.name + ": Destroy object");
                Destroy(obj);
                return;
            }
            obj.SetActive(false);
            obj.transform.SetParent(_poolParent);
            _pool.Enqueue(obj);
        }

        public GameObject Prefab {
            get { return _prefab; }
            set { _prefab = value; }
        }
    }
}