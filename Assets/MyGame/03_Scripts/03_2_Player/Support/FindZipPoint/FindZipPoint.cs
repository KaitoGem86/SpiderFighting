using EasyCharacterMovement;
using UnityEngine;

namespace Core.GamePlay.Player
{
    public class FindZipPoint : MonoBehaviour
    {
        [SerializeField] private RectTransform _detectZipPointArea;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameObject _pointPivot;
        [SerializeField] private GameObject _displayZipPoint;
        [SerializeField] private float _distanceToZipPoint;
        [SerializeField] LayerMask _layerToIgnore;

        private void Update()
        {
            FindZipPointCharacter();
        }

        private void FindZipPointCharacter()
        {
            int layerMask = ~(1 << _layerToIgnore);
            var topScreenPoint = _detectZipPointArea.position + new Vector3(0, _detectZipPointArea.rect.height / 2, 0);
            topScreenPoint.z = _mainCamera.nearClipPlane;
            var topPoint = _mainCamera.ScreenToWorldPoint(topScreenPoint);
            var bottomScreenPoint = _detectZipPointArea.position - new Vector3(0, _detectZipPointArea.rect.height / 2, 0);
            bottomScreenPoint.z = _mainCamera.nearClipPlane;
            var bottomPoint = _mainCamera.ScreenToWorldPoint(bottomScreenPoint);
            var leftScreenPoint = _detectZipPointArea.position - new Vector3(_detectZipPointArea.rect.width / 2, 0, 0);
            leftScreenPoint.z = _mainCamera.nearClipPlane;
            var leftPoint = _mainCamera.ScreenToWorldPoint(leftScreenPoint);
            var rightScreenPoint = _detectZipPointArea.position + new Vector3(_detectZipPointArea.rect.width / 2, 0, 0);
            rightScreenPoint.z = _mainCamera.nearClipPlane;
            var rightPoint = _mainCamera.ScreenToWorldPoint(rightScreenPoint);
            var normalSurface = -_pointPivot.transform.forward;
            topPoint = ProjectPointOntoPlane(topPoint, _pointPivot.transform.position, normalSurface);
            bottomPoint = ProjectPointOntoPlane(bottomPoint, _pointPivot.transform.position, normalSurface);
            leftPoint = ProjectPointOntoPlane(leftPoint, _pointPivot.transform.position, normalSurface);
            rightPoint = ProjectPointOntoPlane(rightPoint, _pointPivot.transform.position, normalSurface);
            var radiusCheckSphereCast = Vector3.Distance(leftPoint, rightPoint) / 2;
            var numberCheckSphereCast = Vector3.Distance(topPoint, bottomPoint) / radiusCheckSphereCast;
            var direction = _pointPivot.transform.forward;
            RaycastHit hitInfo;
            if (Physics.SphereCast(_pointPivot.transform.position, radiusCheckSphereCast, direction, out hitInfo, layerMask))
            {
                var bound = hitInfo.collider.bounds;
                var point = hitInfo.point  + direction * 0.1f;
                point.y += bound.max.y;
                if (Physics.SphereCast(point, 0.01f , Vector3.down, out hitInfo))
                {
                    _displayZipPoint.transform.position = hitInfo.point;
                }
            }
        }

        private Vector3 ProjectPointOntoPlane(Vector3 point, Vector3 planePoint, Vector3 planeNormal)
        {
            var distance = Vector3.Dot(planeNormal, (point - planePoint));
            return point - distance * planeNormal;
        }
    }
}