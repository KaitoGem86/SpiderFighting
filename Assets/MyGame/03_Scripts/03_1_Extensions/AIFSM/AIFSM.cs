using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core.GamePlay.Support;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    public enum AIState{
        None = -1,
        Idle,
        Moving,
        WaitAttack,
        Attack,
        Hit,
        Dead
    }

    [RequireComponent(typeof(BehaviourTreeOwner))]
    public class AIFSM : MonoBehaviour, IHitted{
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
            ChangeAction(AIState.Idle);
        }


        public void ChangeAction(AIState newState){
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

        public void HittedByPlayer(){
            Debug.Log("Hitted by player");
            ChangeAction(AIState.Hit);
        }

        public Transform TargetEnemy{
            get { return this.transform; }
        }
        public BlackBoard blackBoard;
        public AIState currentStateType;
    }
}