using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class PlayerController : FSM<PlayerBlackBoard>{

        protected override void Awake()
        {
            base.Awake();
            currentStateType = FSMState.None;
        }

        public override void ChangeAction(FSMState newState)
        {
            base.ChangeAction(newState);
            blackBoard.CurrentState = _currentState as IPlayerState;
        }

        public void OnCollided(ref CollisionResult collisionResult){
            Debug.Log("OnCollided");
            blackBoard.CurrentState.OnCollided(ref collisionResult);
        }

        public void OnCollisionEnter(Collision collision){
            Debug.Log("OnCollisionEnter");
            blackBoard.CurrentState.OnCollisionEnter(collision);
        }

        public void OnTriggerEnter(Collider other){
            Debug.Log("OnTriggerEnter");
            //blackBoard.CurrentState.OnTriggerEnter(other);
        }
    }
}