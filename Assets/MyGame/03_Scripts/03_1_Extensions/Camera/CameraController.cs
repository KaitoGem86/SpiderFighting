// using System.Linq;
// using Core.Data;
// using Core.GamePlay.Shop;
// using Core.SystemGame;
// using DG.Tweening;
// using Unity.VisualScripting;
using Core.SystemGame;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Rendering.Universal;

namespace Core.GamePlay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 1.0f;
        [SerializeField] private float _damping = -1.0f;
        [SerializeField][Range(0.0f, 1.0f)] private float _inertia;
        [SerializeField] private Transform _cameraRotation;
        [SerializeField] private float _offsetRight;
        [SerializeField] private float _offsetTop;
        [SerializeField] private float _offsetBottom;


        private Vector3 _remainingDelta;
        private Vector3 _lastRemainingDelta;
        private Vector3 _lastMousePosition;
        private float _zoomCameraValue;
        private float _screenWidth;
        private bool _isCanRotate = false;

        private Vector2 _maxTouchPosition;
        private Vector2 _minTouchPosition;

        private void Awake()
        {
            _screenWidth = Screen.width / 4 * 3;
            _minTouchPosition = new Vector3(Screen.width - _screenWidth, _offsetBottom, 0);
            _maxTouchPosition = new Vector3(Screen.width - _offsetRight, Screen.height - _offsetTop, 0);
        }


        private void Start()
        {
        }

        private void OnDestroy()
        {
        }

        public void SetUp()
        {
        }

        private void LateUpdate()
        {
            if (InputSystem.Instance.CheckSelectDown(_InputActionEnum.RotateLooking))
            {
                _lastMousePosition = InputSystem.Instance.GetInputPosition(_InputActionEnum.RotateLooking);
                if (CheckCanRotate(_lastMousePosition))
                {
                    _isCanRotate = true;
                }
                else
                {
                    InputSystem.Instance.UpdateFingerId(_InputActionEnum.RotateLooking, -1);
                }
            }
            else if (InputSystem.Instance.CheckHold(_InputActionEnum.RotateLooking))
            {
                var pos = InputSystem.Instance.GetInputPosition(_InputActionEnum.RotateLooking);
                if (_isCanRotate)
                {
                    Vector3 mouseDelta = pos - _lastMousePosition;
                    _lastMousePosition = pos;
                    _remainingDelta = mouseDelta * _sensitivity * Time.deltaTime;
                }
            }
            else if (InputSystem.Instance.CheckUp(_InputActionEnum.RotateLooking))
            {
                _isCanRotate = false;
            }
            else
            {

            }

            Vector3 remainTmp = Vector3.Lerp(_lastRemainingDelta, _remainingDelta, _inertia);
            var beforeRotation = _cameraRotation.rotation;
            _cameraRotation.Rotate(Vector3.left, remainTmp.y, Space.Self);
            _cameraRotation.Rotate(Vector3.up, remainTmp.x, Space.Self);
            _cameraRotation.rotation = Quaternion.Euler(_cameraRotation.eulerAngles.x, _cameraRotation.eulerAngles.y, 0);
            if (_cameraRotation.eulerAngles.x > 50 && _cameraRotation.eulerAngles.x < 90 || _cameraRotation.eulerAngles.x < 330 && _cameraRotation.eulerAngles.x > 270)
            {
                _cameraRotation.rotation = beforeRotation;
            }

            if (_damping > 0.0f)
            {
                _remainingDelta = Vector3.Lerp(_remainingDelta, Vector3.zero, _damping);
                _zoomCameraValue = Mathf.Lerp(_zoomCameraValue, 0, _damping);
            }
            else
            {
                _remainingDelta = Vector3.zero;
                _zoomCameraValue = 0;
            }
            _lastRemainingDelta = _remainingDelta;
        }

        private bool CheckCanRotate(Vector3 input)
        {
            if (input.x > _minTouchPosition.x && input.x < _maxTouchPosition.x && input.y > _minTouchPosition.y && input.y < _maxTouchPosition.y)
                return true;
            return false;
        }
    }
}
