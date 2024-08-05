using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Support;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    public enum FSMState{
        None = -1,
        Idle,
        Moving,
        WaitAttack,
        Attack,
        Hit,
        Dead
    }

    [RequireComponent(typeof(BehaviourTreeOwner))]
    public class FSM : MonoBehaviour{
        [SerializeField] Transform _stateContainer;
        Dictionary<FSMState, IState> _dictStates;
        private IState _currentState;

        private void Awake(){
            _dictStates = new Dictionary<FSMState, IState>();
            foreach (Transform state in _stateContainer){
                var tmp = state.GetComponent<IState>();
                if (tmp == null){
                    continue;
                }
                _dictStates.Add(tmp.StateType, tmp);
            }
        }

        private void OnEnable(){
            ChangeAction(FSMState.Idle);
        }

        public void ChangeAction(FSMState newState){
            // Change the current state
            if (currentStateType == newState){
                if(_currentState != null && _currentState.CanChangeToItself){
                    _currentState.ExitState();
                    _currentState.EnterState();
                }
                return;
            }
            if (_currentState != null){
                _currentState.ExitState();
            }
            _currentState = _dictStates[newState];
            currentStateType = newState;
            _currentState.EnterState();
        }

        public BlackBoard blackBoard;
        public FSMState currentStateType;
    }
}