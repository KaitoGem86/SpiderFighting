using NodeCanvas.BehaviourTrees;
using UnityEngine;

namespace Extensions.SystemGame.AIFSM{
    [RequireComponent(typeof(BehaviourTreeOwner))]
    public class AIFSM : MonoBehaviour{
        private BaseState _currentState;
        
        public void ChangeAction(BaseState newState){
            // Change the current state
            if(_currentState != null && newState.GetType().Equals(_currentState.GetType())){
                return;
            }
            if (_currentState != null){
                _currentState.ExitState(this);
            }
            _currentState = newState;
            _currentState.EnterState(this);
        }

        public void Update(){
            // Update the current state
            _currentState?.UpdateState(this);
        }

        public BlackBoard blackBoard;
    }
}