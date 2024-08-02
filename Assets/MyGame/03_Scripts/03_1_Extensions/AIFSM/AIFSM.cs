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
        [SerializeField] Transform _stateContainer;
        Dictionary<AIState, IState> _dictStates;
        private IState _currentState;

        private void Awake(){
            _dictStates = new Dictionary<AIState, IState>();
            foreach (Transform state in _stateContainer){
                var tmp = state.GetComponent<IState>();
                if (tmp == null){
                    continue;
                }
                _dictStates.Add(tmp.StateType, tmp);
            }
            Debug.Log(_dictStates.Count);
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