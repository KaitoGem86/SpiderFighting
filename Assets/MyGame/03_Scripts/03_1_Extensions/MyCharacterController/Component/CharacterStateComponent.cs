using System.Collections.Generic;
using EasyCharacterMovement;
using UnityEngine;

namespace Extensions.SystemGame.MyCharacterController
{
    public class CharacterStateComponent<T> : BaseCharacterComponent<T>, ICharacterLoop where T : MyCharacterController<T>
    {
        [SerializeField] List<BaseCharacterAction<T>> _listActions;
        private Dictionary<ActionEnum, BaseCharacterAction<T>> _dictActions;

        private ActionEnum _currentAction;
        public override void Init(CharacterBlackBoard<T> controller)
        {
            base.Init(controller);
            InitDictAction(_controller);
        }

        private void InitDictAction(MyCharacterController<T> controller)
        {
            _dictActions = new Dictionary<ActionEnum, BaseCharacterAction<T>>();
            foreach (var item in _listActions)
            {
                _dictActions.Add(item.ActionEnum, item);
                item.Init(controller, item.ActionEnum);
            }
            
        }

        public void ChangeAction(ActionEnum action)
        {
            ChangeMovement(action);
        }

        private void ChangeMovement(ActionEnum action)
        {
            if (_currentAction == action)
            {
                if (_dictActions[_currentAction].CanChangeToItself)
                {
                    _dictActions[_currentAction].Exit(action);
                    _dictActions[_currentAction].Enter(_currentAction);
                }
                return;
            }

            if (_dictActions[_currentAction].Exit(action))
            {
                var beforeAction = _currentAction;
                _currentAction = action;
                _dictActions[_currentAction].Enter(beforeAction);
            }
        }

        public void Update()
        {
            _dictActions[_currentAction].Update();
        }

        public void LateUpdate()
        {
            _dictActions[_currentAction].LateUpdate();
        }

        public void FixedUpdate()
        {
            _dictActions[_currentAction].FixedUpdate();
        }

        public override void OnCollisionEnter(UnityEngine.Collision collision)
        {
            _dictActions[_currentAction].OnCollisionEnter(collision);
        }

        public override void OnCollisionStay(UnityEngine.Collision collision)
        {
            _dictActions[_currentAction].OnCollisionStay(collision);
        }

        public override void OnCollisionExit(UnityEngine.Collision collision)
        {
            _dictActions[_currentAction].OnCollisionExit(collision);
        }

        public override void OnTriggerEnter(Collider other)
        {
            _dictActions[_currentAction].OnTriggerEnter(other);
        }

        public override void OnTriggerStay(Collider other)
        {
            _dictActions[_currentAction].OnTriggerStay(other);
        }

        public override void OnTriggerExit(Collider other)
        {
            _dictActions[_currentAction].OnTriggerExit(other);
        }

        public override void OnCollided(ref CollisionResult collisionResult)
        {
            _dictActions[_currentAction].OnCollided(ref collisionResult);
        }
    }
}