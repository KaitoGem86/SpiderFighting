using System.Collections.Generic;
using System.Net.Http.Headers;
using AYellowpaper.SerializedCollections;
using MyTools.ScreenSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    public enum StateTypeEnum
    {
        Movement,
        Interaction
    }


    public class PlayerStateComponent : BasePlayerComponent, IPlayerLoop
    {
        [SerializeField] private SerializedDictionary<ActionEnum, BasePlayerAction> _dictPlayerMovementActions;

        private ActionEnum _currentAction = ActionEnum.None;
        public override void Init(PlayerController playerController)
        {
            _currentAction = ActionEnum.None;
            base.Init(playerController);
            InitDictAction(_playerController);
            InitEvent(_playerController);
        }

        private void InitDictAction(PlayerController playerController)
        {
            foreach (var item in _dictPlayerMovementActions)
            {
                item.Value.Init(playerController, item.Key);
            }
            ChangeAction(ActionEnum.Idle);
        }

        private void InitEvent(PlayerController playerController)
        {
        }

        public void ChangeAction(ActionEnum action)
        {
            ChangeMovement(action);
        }

        private void ChangeMovement(ActionEnum action)
        {
            if (_currentAction == action)
            {
                if (_dictPlayerMovementActions[_currentAction].CanChangeToItself)
                {
                    _dictPlayerMovementActions[_currentAction].Enter(_currentAction);
                }
                return;
            }

            if (_currentAction == ActionEnum.None || _dictPlayerMovementActions[_currentAction].Exit(action))
            {
                var beforeAction = _currentAction;
                _currentAction = action;
                _dictPlayerMovementActions[_currentAction].Enter(beforeAction);
            }
        }


        public void Update()
        {
            _dictPlayerMovementActions[_currentAction].Update();
        }

        public void LateUpdate()
        {
            _dictPlayerMovementActions[_currentAction].LateUpdate();
        }

        public void FixedUpdate()
        {
            _dictPlayerMovementActions[_currentAction].FixedUpdate();
        }

        public override void OnCollisionEnter(UnityEngine.Collision collision)
        {
            _dictPlayerMovementActions[_currentAction].OnCollisionEnter(collision);
        }

        public override void OnCollisionStay(UnityEngine.Collision collision)
        {
            _dictPlayerMovementActions[_currentAction].OnCollisionStay(collision);
        }

        public override void OnCollisionExit(UnityEngine.Collision collision)
        {
            _dictPlayerMovementActions[_currentAction].OnCollisionExit(collision);
        }

        public void Zip()
        {
            ChangeAction(ActionEnum.Zip);
        }

        private void ApplyVerticalVelocity()
        {
            if (VerticalVelocityValue > 0)
            {
                if (_playerController.CharacterMovement.isGrounded)
                {
                    if (Physics.Raycast(_playerController.CharacterMovement.transform.position, Vector3.down, out RaycastHit hit))
                    {
                        if (Vector3.Angle(hit.normal, Vector3.up) < 45f)
                        {
                            VerticalVelocityValue = 0;
                            return;
                        }
                        else
                        {
                            //VerticalVelocityValue = Vector3.ProjectOnPlane(VerticalVelocityValue * Vector3.down, hit.normal).magnitude;
                            VerticalVelocityValue += 9.8f * Time.deltaTime * 2;
                            _playerController.CharacterMovement.Move(Vector3.ProjectOnPlane(VerticalVelocityValue * Vector3.down, hit.normal) * Time.deltaTime);
                            return;
                        }
                    }
                    else
                    {
                        VerticalVelocityValue = 0;
                        return;
                    }
                }
                if (!UseGravity)
                {
                    VerticalVelocityValue = 0;
                    return;
                }
            }
            VerticalVelocityValue += 9.8f * Time.deltaTime;
            _playerController.CharacterMovement.Move(Vector3.down * VerticalVelocityValue * Time.deltaTime);
        }
        public Vector3 SurfaceNormal;
        public float VerticalVelocityValue { get; set; }
        public bool UseGravity { get; set; } = true;
        public ActionEnum CurrentMovementAction => _currentAction;
        public BasePlayerAction GetAction(ActionEnum actionEnum)
        {
            return GetStateTypeEnum(actionEnum) switch
            {
                StateTypeEnum.Movement => _dictPlayerMovementActions[actionEnum],
                _ => null
            };
        }

        private StateTypeEnum GetStateTypeEnum(ActionEnum actionEnum)
        {
            if(actionEnum == ActionEnum.Landing) {
                Debug.Log(_currentAction);
            }
            return actionEnum switch
            {
                ActionEnum.PickingUp => StateTypeEnum.Interaction,
                ActionEnum.Holding => StateTypeEnum.Interaction,
                ActionEnum.None => StateTypeEnum.Interaction,
                ActionEnum.HoldMelee => StateTypeEnum.Interaction,
                _ => StateTypeEnum.Movement
            };
        }

    }
}