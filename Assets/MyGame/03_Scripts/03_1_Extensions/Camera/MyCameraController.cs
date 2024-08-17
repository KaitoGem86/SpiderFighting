using UnityEngine;
using SFRemastered.InputSystem;

namespace Extensions.MyCamera{
    public class MyCameraController : MonoBehaviour{
        public GameObject CinemachineCameraTarget;
        public Transform DefaultCameraPivot;
        public float TopClamp = 70.0f;
        public float BottomClamp = -30.0f;
        public float CameraAngleOverride = 0.0f;
        public bool LockCameraPosition = false;
        // cinemachine
        public float _cinemachineTargetYaw;
        public float _cinemachineTargetPitch;
        private Vector2 look = Vector2.zero;
        private const float _threshold = 0.01f;

        private void Update()
        {
            look.x = InputManager.instance.look.y * 70f;
            look.y = -InputManager.instance.look.x * 70f;
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            // if (TimeManager.instance.pause || InputManager.instance.disableInput)
            //     return;

            if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                float deltaTimeMultiplier = Time.deltaTime;

                _cinemachineTargetYaw += look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += look.y * deltaTimeMultiplier;
            }

            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        
            if(look.sqrMagnitude < _threshold)
            {
                var targetForward = DefaultCameraPivot.transform.forward;
                CinemachineCameraTarget.transform.rotation = Quaternion.Slerp(CinemachineCameraTarget.transform.rotation, Quaternion.LookRotation(targetForward), 0.1f);   
            }




        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}