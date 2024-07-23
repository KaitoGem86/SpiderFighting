using UnityEngine;
using UnityEngine.Analytics;

namespace Core.GamePlay.Support
{
    public class NewFindZipPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _displayZipPoint;
        [SerializeField] private Transform _playerController;
        [SerializeField] private float _radius;
        [SerializeField] private RectTransform _focusPanel;
        [SerializeField] private RectTransform _centerPoint;
        [SerializeField] private Camera _cameraController;

        private Vector3 _viewPortFocusPanelPosition;
        private Vector2 _viewPortFocusPanelSize;
        private Vector3 _viewPortCenterPointPosition;

        private void Awake()
        {
            InitFocusPanel();
        }

        private void Update()
        {
            var collider = Physics.OverlapSphere(_playerController.position, _radius);
            Vector3 closestPoint = Vector3.negativeInfinity;
            Vector3 closestPointInFocusPanel = Vector3.negativeInfinity;
            bool isFound = false;
            bool isFoundInFocusPanel = false;
            float minDistance = float.MaxValue;
            float minDistanceInFocusPanel = float.MaxValue;
            foreach (var col in collider)
            {
                var tmp = col.GetComponent<ZipPointOnObject>();
                if (tmp != null)
                {
                    var res = tmp.GetZipPoint(_cameraController);
                    var points = res;
                    foreach (var point in points)
                    {
                        if (!CheckValidZipPoint(point.Item1))
                        {
                            continue;
                        }
                        if (CheckInFocusPanel(point.Item1))
                        {
                            var distance1 = point.Item2;
                            if (distance1 < minDistanceInFocusPanel)
                            {
                                isFoundInFocusPanel = true;
                                minDistanceInFocusPanel = distance1;
                                closestPointInFocusPanel = point.Item1;
                            }
                            continue;
                        }

                        var distance = point.Item2;
                        if (distance < minDistance)
                        {
                            isFound = true;
                            minDistance = distance;
                            closestPoint = point.Item1;
                        }
                    }
                }
            }

            if(isFoundInFocusPanel){
                _displayZipPoint.SetActive(true);
                _displayZipPoint.transform.position = closestPointInFocusPanel;
                return;
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

        private bool CheckValidZipPoint(Vector3 point)
        {
            var viewPoint = _cameraController.WorldToViewportPoint(point);
            return viewPoint.x > 0 && viewPoint.x < 1 && viewPoint.y > 0 && viewPoint.y < 1 && viewPoint.z > 0;
        }

        private bool CheckInFocusPanel(Vector3 point)
        {
            var viewPoint = _cameraController.WorldToViewportPoint(point);
            return viewPoint.x > (_viewPortFocusPanelPosition.x - _viewPortFocusPanelSize.x / 2)
                && viewPoint.x < (_viewPortFocusPanelPosition.x + _viewPortFocusPanelSize.x / 2)
                && viewPoint.y > (_viewPortFocusPanelPosition.y - _viewPortFocusPanelSize.y / 2)
                && viewPoint.y < (_viewPortFocusPanelPosition.y + _viewPortFocusPanelSize.y / 2)
                && viewPoint.z > 0;
        }

        private void InitFocusPanel()
        {
            _viewPortFocusPanelPosition = _cameraController.ScreenToViewportPoint(_focusPanel.position);
            var rightTop = _cameraController.ScreenToViewportPoint(_focusPanel.position + new Vector3(_focusPanel.rect.width / 2, _focusPanel.rect.height / 2, 0));
            var leftBottom = _cameraController.ScreenToViewportPoint(_focusPanel.position - new Vector3(_focusPanel.rect.width / 2, _focusPanel.rect.height / 2, 0));
            _viewPortFocusPanelSize = new Vector2(rightTop.x - leftBottom.x, rightTop.y - leftBottom.y);
            _viewPortCenterPointPosition = _cameraController.ScreenToViewportPoint(_centerPoint.position);
        }
    }
}