using UnityEngine;

namespace Core.GamePlay.Support
{
    public class NewFindZipPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _displayZipPoint;
        [SerializeField] private Transform _playerController;
        [SerializeField] private float _radius;

        private void Update()
        {
            var collider = Physics.OverlapSphere(_playerController.position, _radius);
            Vector3 closestPoint = Vector3.negativeInfinity;
            bool isFound = false;
            float minDistance = float.MaxValue;
            foreach (var col in collider)
            {
                var tmp = col.GetComponent<ZipPointOnBuilding>();
                if (tmp != null)
                {
                    var point = tmp.GetZipPoint(_playerController);
                    var distance = Vector3.Distance(_playerController.position, point);
                    if (distance < minDistance)
                    {
                        isFound = true;
                        minDistance = distance;
                        closestPoint = point;
                    }
                }
            }
            if (isFound)
            {
                _displayZipPoint.SetActive(true);
                _displayZipPoint.transform.position = closestPoint;
            }
            else
            {
                _displayZipPoint.SetActive(false);
            }
        }
    }
}