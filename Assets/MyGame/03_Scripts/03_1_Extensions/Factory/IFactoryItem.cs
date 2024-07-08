using UnityEngine;

namespace Core.SystemGame.Factory{
    public interface IFactoryItem{
        GameObject Spawn(Vector3 position = default, bool isKinematic = false);
    }
}