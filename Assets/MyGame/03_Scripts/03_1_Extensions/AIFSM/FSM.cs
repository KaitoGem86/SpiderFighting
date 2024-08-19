using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Support;
using NodeCanvas.BehaviourTrees;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    [System.Serializable]
    public enum FSMState{
        None = -1,
        Spawn,
        Idle,
        Moving,
        WaitAttack,
        Attack,
        Hit,
        Dead,
        StopMoving,
        Landing,
        Jumping,
        Climbing,
        FallingDown,
        Dive,
        Swing,
        JumpFromSwing,
        StartAttack,
        LastAttack,
        Dodge,
        UltimateAttack,
        UseGadget,
        KnockBack,
        StunLock,
        AIM,
    }

    public class FSM<T> : MonoBehaviour, IFSM where T : BlackBoard{
        [SerializeField] Transform _stateContainer;
        [SerializeField] protected FSMState _startState;
        Dictionary<FSMState, IState> _dictStates;
        protected IState _currentState;

        protected virtual void Awake(){
            _dictStates = new Dictionary<FSMState, IState>();
            foreach (Transform state in _stateContainer){
                var tmp = state.GetComponent<IState>();
                if (tmp == null){
                    continue;
                }
                _dictStates.Add(tmp.StateType, tmp);
            }
        }

        protected virtual void OnEnable(){
            ChangeAction(_startState);
        }

        public virtual void ChangeAction(FSMState newState){
            // Change the current state
            if (currentStateType == newState){
                if(_currentState != null && _currentState.CanChangeToItself){
                    _currentState.ExitState();
                    _currentState.EnterState();
                }
                return;
            }
            if (currentStateType != FSMState.None && _currentState != null){
                _currentState.ExitState();
            }
            _currentState = _dictStates[newState];
            currentStateType = newState;
            _currentState.EnterState();
        }

        public T blackBoard;
        public FSMState currentStateType;
    }
}