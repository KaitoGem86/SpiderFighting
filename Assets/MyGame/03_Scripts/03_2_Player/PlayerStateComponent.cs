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

        [SerializeField] private SerializedDictionary<ActionEnum, BasePlayerAction> _dictPlayerInteractionActions;
        private ActionEnum _currentMovementAction = ActionEnum.None;
        private ActionEnum _currentInteractionAction = ActionEnum.None;
        private ActionEnum _beforeMovementAction;
        public override void Init(PlayerController playerController)
        {
            _currentMovementAction = ActionEnum.None;
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
            foreach (var item in _dictPlayerInteractionActions)
            {
                item.Value.Init(playerController,
                item.Key);
            }
            ChangeAction(ActionEnum.Idle);
            ChangeAction(ActionEnum.None);
        }

        private void InitEvent(PlayerController playerController)
        {
        }

        public bool ChangeAction(ActionEnum action)
        {
            if (GetStateTypeEnum(action) == StateTypeEnum.Movement)
            {
                return ChangeMovement(action);
            }
            else
            {
                return ChangeInteraction(action);
            }
        }

        private bool ChangeMovement(ActionEnum action)
        {
            if (_currentMovementAction == action)
            {
                if (_dictPlayerMovementActions[_currentMovementAction].CanChangeToItself)
                {
                    _dictPlayerMovementActions[_currentMovementAction].Enter();
                    return true;
                }
                return false;
            }

            if (_currentMovementAction == ActionEnum.None || _dictPlayerMovementActions[_currentMovementAction].Exit(action))
            {
                _beforeMovementAction = _currentMovementAction;
                _currentMovementAction = action;
                _dictPlayerMovementActions[_currentMovementAction].Enter();
                return true;
            }
            else
                return false;
        }

        private bool ChangeInteraction(ActionEnum action)
        {
            if (_currentInteractionAction == action)
            {
                if (_dictPlayerInteractionActions[_currentInteractionAction].CanChangeToItself)
                {
                    Debug.Log("ChangeInteraction");
                    _dictPlayerInteractionActions[_currentInteractionAction].Enter();
                    return true;
                }
                return false;
            }
            if (_dictPlayerInteractionActions[_currentInteractionAction].Exit(action))
            {
                _currentInteractionAction = action;
                _dictPlayerInteractionActions[_currentInteractionAction].Enter();
                return true;
            }
            else
                return false;
        }

        public void Update()
        {
            //Debug.Log(_currentAction.ToString());
            _dictPlayerMovementActions[_currentMovementAction].Update();
            _dictPlayerInteractionActions[_currentInteractionAction].Update();
        }

        public void LateUpdate()
        {
            _dictPlayerMovementActions[_currentMovementAction].LateUpdate();
            _dictPlayerInteractionActions[_currentInteractionAction].LateUpdate();
        }

        public void FixedUpdate()
        {
            _dictPlayerMovementActions[_currentMovementAction].FixedUpdate();
            _dictPlayerInteractionActions[_currentInteractionAction].FixedUpdate();
            ApplyVerticalVelocity();
        }

        public override void OnCollisionEnter(UnityEngine.Collision collision)
        {
            _dictPlayerMovementActions[_currentMovementAction].OnCollisionEnter(collision);
            _dictPlayerInteractionActions[_currentInteractionAction].OnCollisionEnter(collision);
        }

        public override void OnCollisionStay(UnityEngine.Collision collision)
        {
            _dictPlayerMovementActions[_currentMovementAction].OnCollisionStay(collision);
            _dictPlayerInteractionActions[_currentInteractionAction].OnCollisionStay(collision);
        }

        public override void OnCollisionExit(UnityEngine.Collision collision)
        {
            _dictPlayerMovementActions[_currentMovementAction].OnCollisionExit(collision);
            _dictPlayerInteractionActions[_currentInteractionAction].OnCollisionExit(collision);
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

        // public void SetTargetInteractableObject(InteractInfo info)
        // {
        //     TargetInteractableObject = info.interactableObject;
        // }

        public float VerticalVelocityValue { get; set; }
        public bool UseGravity { get; set; } = true;
        public ActionEnum CurrentMovementAction => _currentMovementAction;
        public ActionEnum CurrentInteractionAction => _currentInteractionAction;
        public BasePlayerAction GetAction(ActionEnum actionEnum)
        {
            return GetStateTypeEnum(actionEnum) switch
            {
                StateTypeEnum.Interaction => _dictPlayerInteractionActions[actionEnum],
                StateTypeEnum.Movement => _dictPlayerMovementActions[actionEnum],
                _ => null
            };
        }

        private StateTypeEnum GetStateTypeEnum(ActionEnum actionEnum)
        {
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