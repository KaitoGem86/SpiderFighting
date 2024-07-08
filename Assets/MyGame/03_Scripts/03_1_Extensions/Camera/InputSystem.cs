using System.Collections;
using UnityEngine;

namespace Core.SystemGame
{
    public enum _InputActionEnum
    {
        None,
        Moving,
        RotateLooking,
    }

    public class InputSystem
    {
        private static InputSystem _instance;

        public static InputSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputSystem();
                }
                return _instance;
            }
        }

        private int _movingFingerId = -1;
        private int _rotateLookingFingerId = -1;

        public float Timer { get; set; }

        public Vector3 GetInputPosition(_InputActionEnum type)
        {
            int tmp = GetFingerId(type);
            if (tmp == -1)
            {
                return Vector3.positiveInfinity;
            }
            return GetTouchId(tmp).position;
        }

        public bool CheckSelectDown(_InputActionEnum type)
        {
            int tmp = GetFingerId(type);
            if (tmp != -1)
            {
                return false;
            }
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch tmpFinger = Input.GetTouch(i);
                if (tmpFinger.phase == TouchPhase.Began)
                {
                    UpdateFingerId(type, tmpFinger.fingerId);
                    return true;
                }
            }
            return false;
        }

        public bool CheckHold(_InputActionEnum type)
        {
            int tmp = GetFingerId(type);
            if (tmp == -1)
            {
                return false;
            }
            Touch touch = GetTouchId(tmp);
            if (touch.phase != TouchPhase.Began && touch.phase != TouchPhase.Ended)
            {
                return true;
            }
            else
            {
                Timer = 0;
                return false;
            }
        }

        public bool CheckUp(_InputActionEnum type)
        {
            int tmp = GetFingerId(type);
            if (tmp == -1)
            {
                return false;
            }
            Touch touch = GetTouchId(tmp);
            if (touch.phase == TouchPhase.Ended)
            {
                UpdateFingerId(type, -1);
                return true;
            }
            return false;
        }

        public void UpdateFingerId(_InputActionEnum type, int fingerId)
        {
            switch (type)
            {
                case _InputActionEnum.Moving:
                    _movingFingerId = fingerId;
                    break;
                case _InputActionEnum.RotateLooking:
                    _rotateLookingFingerId = fingerId;
                    break;
            }
        }

        private Touch GetTouchId(int fingerId)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).fingerId == fingerId)
                {
                    return Input.GetTouch(i);
                }
            }
            return default;
        }

        private int GetFingerId(_InputActionEnum type)
        {
            return type switch
            {
                _InputActionEnum.Moving => _movingFingerId,
                _InputActionEnum.RotateLooking => _rotateLookingFingerId,
                _ => -1,
            };
        }

        public Joystick InputJoyStick { get; set; }
        public bool IsSprint { get; set; }
        public bool IsJump { get; set; }
    }
}
