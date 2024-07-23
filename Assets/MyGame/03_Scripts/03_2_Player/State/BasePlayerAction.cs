using System;
using System.Collections.Generic;
using Animancer;
using AYellowpaper.SerializedCollections;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.Events;

namespace Core.GamePlay.Player
{
    public enum PriorityEnum
    {
        None,
        Low,
        Medium,
        High,
        Critical
    }

    public enum ActionEnum
    {
        None,
        Idle,
        Moving,
        Jumping,
        FallingDown,
        Sprinting,
        Sliding,
        Landing,
        StopMoving,
        PickingUp,
        Holding,
        HoldMelee,
        Cooking,
        Die,
        Swing,
        JumpFromSwing,
        Climbing,
        Dive,
        Zip,
        Spawn
    }

    [Serializable]
    public class BasePlayerAction : ScriptableObject, IPlayerAction
    {
        protected PlayerController _playerController;
        protected ActionEnum _actionEnum;
        protected PlayerStateComponent _stateContainer;
        protected PlayerDisplayComponent _displayContainer;
        protected PlayerStatComponent _statManager;
        [SerializeField] MovementMode _movementMode;
        [SerializeField] protected ClipTransition _animationClip;
        [SerializeField] protected bool _fixedAnim;
        [SerializeField] private bool _canChangeToItself = false;
        [SerializeField] protected SerializedDictionary<ActionEnum, List<PlayerAnimTransition>> _dictPlayerAnimTransition;
        protected AnimancerState _state;
        protected PlayerAnimTransition _currentTransition;
        private int _randomTransition;

        public virtual void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            _playerController = playerController;
            _actionEnum = actionEnum;
            _stateContainer = _playerController.ResolveComponent<PlayerStateComponent>(PlayerComponentEnum.State);
            _displayContainer = _playerController.ResolveComponent<PlayerDisplayComponent>(PlayerComponentEnum.Display);
            _statManager = _playerController.ResolveComponent<PlayerStatComponent>(PlayerComponentEnum.Stat);
        }

        public virtual void Enter(ActionEnum actionBefore)
        {
            _randomTransition = GetTransition(actionBefore);
            _currentTransition = _dictPlayerAnimTransition[_fixedAnim ? ActionEnum.None : actionBefore][_randomTransition];
            _playerController.SetMovementMode(_movementMode);
            if(_movementMode == MovementMode.None)
            {
                _playerController.CharacterMovement.rigidbody.useGravity = true;
                _playerController.CharacterMovement.rigidbody.isKinematic = false;
                _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                _playerController.CharacterMovement.rigidbody.velocity = _playerController.GlobalVelocity;
            }
            else{
                _playerController.CharacterMovement.rigidbody.useGravity = false;
                _playerController.CharacterMovement.rigidbody.isKinematic = true;
                _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                _playerController.SetVelocity(_playerController.GlobalVelocity);
            }
            StartAction();
        }

        public virtual bool Exit(ActionEnum actionAfter)
        {
            _playerController.GlobalVelocity = _movementMode == MovementMode.None ? _playerController.CharacterMovement.rigidbody.velocity : _playerController.GetVelocity();
            _state = null;
            return true;
        }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void OnCollisionEnter(Collision collision) { }

        public virtual void OnCollisionExit(Collision collision) { }

        public virtual void OnCollisionStay(Collision collision) { }

        public virtual void OnTriggerEnter(Collider other) { }

        public virtual void OnTriggerExit(Collider other) { }

        public virtual void OnTriggerStay(Collider other) { }
        public virtual void OnCollided(ref CollisionResult collisionResult){}

        public bool CanChangeToItself => _canChangeToItself;

        protected void StartAction()
        {
            if (_currentTransition.startAnimation.Clip != null)
            {
                _displayContainer.PlayAnimation(_currentTransition.startAnimation);
            }
            else
            {
                KeepAction();
            }
        }

        public virtual void KeepAction()
        {
            if(_currentTransition.keepAnimation.Animations.Length == 0) return;
            _displayContainer.PlayAnimation(_currentTransition.keepAnimation);
        }

        public void EndAction()
        {
            if(_currentTransition.endAnimation.Clip == null)
            {
                ExitAction();
                return;
            }
            _displayContainer.PlayAnimation(_currentTransition.endAnimation);
            //_currentTransition.endAnimation.Events.OnEnd += ExitAction;
        }

        public virtual void ExitAction(){
        }

        protected virtual int GetTransition(ActionEnum actionBefore)
        {
            return UnityEngine.Random.Range(0, _dictPlayerAnimTransition[_fixedAnim ? ActionEnum.None : actionBefore].Count);
        }
    }
}