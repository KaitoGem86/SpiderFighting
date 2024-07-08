using System;
using Animancer;
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
        StartMoving,
        StopMoving,
        PickingUp,
        Holding,
        HoldMelee,
        CuttingTree,
        InteractWithObject,
        Cooking,
        Die,
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
        [SerializeField] protected PriorityEnum _priority;
        [SerializeField] private bool _canChangeToItself = false;
        [SerializeField] private float _healthCostPerFixedUpdate;
        protected AnimancerState _state;

        public virtual void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            _playerController = playerController;
            _actionEnum = actionEnum;
            _stateContainer = _playerController.ResolveComponent<PlayerStateComponent>(PlayerComponentEnum.State);
            _displayContainer = _playerController.ResolveComponent<PlayerDisplayComponent>(PlayerComponentEnum.Display);
            _statManager = _playerController.ResolveComponent<PlayerStatComponent>(PlayerComponentEnum.Stat);
        }

        public virtual void Enter()
        {
            // if (_state == null)
            // {
            _state = _displayContainer.PlayAnimation(_animationClip.Clip, _animationClip.FadeDuration, PlayerTypeAnimMask.Base);
            _state.Speed = _animationClip.Speed;
            _state.Events = _animationClip.Events;
            if (Priortiy == PriorityEnum.Critical)
            {
                _stateContainer.ChangeAction(ActionEnum.None);
            }
            // }
            // else{
            //     _state.Time = 0;
            //     _state.StartFade(1, _animationClip.FadeDuration);
            // }
        }

        public virtual bool Exit(ActionEnum actionAfter)
        {
            _state = null;
            return true;
        }

        public virtual void Update()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void FixedUpdate()
        {
            _statManager.OnUpdatePlayerRuntimeValue(PlayerStatType.Health, -_healthCostPerFixedUpdate, false);
            //throw new System.NotImplementedException();
        }

        public virtual void LateUpdate()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnCollisionExit(Collision collision)
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnCollisionStay(Collision collision)
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnTriggerExit(Collider other)
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnTriggerStay(Collider other)
        {
            //throw new System.NotImplementedException();
        }

        public PriorityEnum Priortiy => _priority;
        public bool CanChangeToItself => _canChangeToItself;

        // private Action RunEvent(int i){
        //     return  _actionEvents[i].Event.Invoke;
        // }
    }
}