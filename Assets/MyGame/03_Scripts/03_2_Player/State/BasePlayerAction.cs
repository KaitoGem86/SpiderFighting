using System;
using System.Collections.Generic;
using Animancer;
using AYellowpaper.SerializedCollections;
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
        Zip
    }

    [Serializable]
    public class BasePlayerAction : ScriptableObject, IPlayerAction
    {
        protected PlayerController _playerController;
        protected ActionEnum _actionEnum;
        protected PlayerStateComponent _stateContainer;
        protected PlayerDisplayComponent _displayContainer;
        protected PlayerStatComponent _statManager;
        [SerializeField] protected ClipTransition _animationClip;
        [SerializeField] protected bool _fixedAnim;
        [SerializeField] private bool _canChangeToItself = false;
        [SerializeField] protected SerializedDictionary<ActionEnum, List<PlayerAnimTransition>> _dictPlayerAnimTransition;
        protected AnimancerState _state;
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
            _randomTransition = GetRandomTransition(actionBefore);
            StartAction(actionBefore, _randomTransition);
        }

        public virtual bool Exit(ActionEnum actionAfter)
        {
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

        public bool CanChangeToItself => _canChangeToItself;

        protected void StartAction(ActionEnum actionBefore, int index)
        {
            if (_dictPlayerAnimTransition[actionBefore][index].startAnimation.Clip != null)
            {
                _state = _displayContainer.PlayAnimation(_dictPlayerAnimTransition[actionBefore][index].startAnimation);
                _state.Events.OnEnd += () => KeepAction(actionBefore, index);
            }
            else
            {
                KeepAction(actionBefore, index);
            }
        }

        protected virtual void KeepAction(ActionEnum actionBefore, int index)
        {
            if(_state != null)
                _state.Events.OnEnd = null;
            _state = _displayContainer.PlayAnimation(_dictPlayerAnimTransition[_fixedAnim ? ActionEnum.None : actionBefore][index].keepAnimation);
        }

        protected void EndAction(ActionEnum actionBefore, int index)
        {
            if(_dictPlayerAnimTransition[actionBefore][index].endAnimation.Clip == null)
            {
                ExitAction();
                return;
            }
            _state.Events.OnEnd = null;
            _state = _displayContainer.PlayAnimation(_dictPlayerAnimTransition[_fixedAnim ? ActionEnum.None : actionBefore][index].endAnimation);
            _state.Events.OnEnd += ExitAction;
        }

        protected virtual void ExitAction(){}

        private int GetRandomTransition(ActionEnum actionBefore)
        {
            return UnityEngine.Random.Range(0, _dictPlayerAnimTransition[_fixedAnim ? ActionEnum.None : actionBefore].Count);
        }
    }
}