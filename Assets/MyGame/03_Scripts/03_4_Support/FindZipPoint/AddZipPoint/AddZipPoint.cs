#if UNITY_EDITOR
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class AddZipPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabWithZipPoint;
        [SerializeField] private Transform _parent;

        [ContextMenu("Change to Prefab with zip point")]
        public void ChangeToPrefabWithZipPoint()
        {
            foreach (Transform child in _parent)
            {
                Vector3 position = child.position;
                Quaternion rotation = child.rotation;
                DestroyImmediate(child.gameObject);
                //Destroy(child.gameObject);

                GameObject newObject = Instantiate(_prefabWithZipPoint, position, rotation, _parent);
                newObject.name = _prefabWithZipPoint.name;
            }
        }
    }
}
#endif