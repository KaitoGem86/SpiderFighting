using UnityEngine;

namespace Core.GamePlay.Support
{
    public class CameraDetectZipPoint : MonoBehaviour
    {
        [SerializeField] private float _distanceRange;
        [SerializeField] private RectTransform _displayZipPoint;
        [SerializeField] private LayerMask _layerToZip;
        [SerializeField] private LayerMask _groundLayer;

        public void Update()
        {
            Debug.DrawRay(transform.position, transform.forward * _distanceRange, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _distanceRange, _layerToZip))
            {
                _displayZipPoint.gameObject.SetActive(true);
                if (hit.collider.gameObject.layer != _groundLayer)
                {
                    _displayZipPoint.position = Camera.main.WorldToScreenPoint(hit.point);
                }
            }
            else
            {
                _displayZipPoint.gameObject.SetActive(false);
            }
        }
    }
}