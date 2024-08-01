using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    public enum AIState{
        None = -1,
        Idle,
        Moving,
        WaitAttack,
        Attack,
        Dead
    }

    [RequireComponent(typeof(BehaviourTreeOwner))]
    public class AIFSM : MonoBehaviour{
        [SerializeField] List<BaseState> _states;
        Dictionary<AIState, BaseState> _dictStates;
        private BaseState _currentState;

        private void Awake(){
            _dictStates = new Dictionary<AIState, BaseState>();
            foreach (var state in _states){
                _dictStates.Add(state.stateType, state);
            }
            ChangeAction(AIState.Idle);
        }


        public void ChangeAction(AIState newState){
            // Change the current state
            if (currentStateType == newState){
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
        public AIState currentStateType;
    }
}