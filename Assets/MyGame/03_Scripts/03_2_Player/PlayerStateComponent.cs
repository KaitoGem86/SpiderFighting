using System.Collections.Generic;
using System.Net.Http.Headers;
using AYellowpaper.SerializedCollections;
using EasyCharacterMovement;
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
        }

        private void InitDictAction(PlayerController playerController)
        {
            foreach (var item in _dictPlayerMovementActions)
            {
                item.Value.Init(playerController, item.Key);
            }
            ChangeAction(ActionEnum.Spawn);
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
                    _dictPlayerMovementActions[_currentAction].Exit(action);
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

        public override void OnTriggerEnter(Collider other)
        {
            _dictPlayerMovementActions[_currentAction].OnTriggerEnter(other);
        }

        public override void OnTriggerStay(Collider other)
        {
            _dictPlayerMovementActions[_currentAction].OnTriggerStay(other);
        }

        public override void OnTriggerExit(Collider other)
        {
            _dictPlayerMovementActions[_currentAction].OnTriggerExit(other);
        }

        public override void OnCollided(ref CollisionResult collisionResult)
        {
            _dictPlayerMovementActions[_currentAction].OnCollided(ref collisionResult);
        }

        public void Zip()
        {
            if (_currentAction == ActionEnum.Swing || _currentAction == ActionEnum.Zip) return;
            ChangeAction(ActionEnum.Zip);
        }

        public Vector3 SurfaceNormal;
        public ActionEnum CurrentMovementAction => _currentAction;
    }
}