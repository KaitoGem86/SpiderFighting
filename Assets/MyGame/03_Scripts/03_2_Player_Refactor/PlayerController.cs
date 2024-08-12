using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class PlayerController : FSM<PlayerBlackBoard>
    {

        protected override void Awake()
        {
            base.Awake();
            currentStateType = FSMState.None;
        }

        protected override void OnEnable(){
            OnChangePlayerModel(blackBoard.PlayerData.playerSerializeData.skinIndex); 
            blackBoard.GadgetsController.ChangeGadget(blackBoard.PlayerData.playerSerializeData.gadgetIndex);
            base.OnEnable();
        }

        public override void ChangeAction(FSMState newState)
        {
            base.ChangeAction(newState);
            blackBoard.CurrentState = _currentState as IPlayerState;
        }

        public void OnCollided(ref CollisionResult collisionResult)
        {
            blackBoard.CurrentState?.OnCollided(ref collisionResult);
        }

        public void OnCollisionEnter(Collision collision)
        {
            blackBoard.CurrentState.OnCollisionEnter(collision);
        }

        public void OnChangePlayerModel(int index)
        {
            var playerModel = blackBoard.CurrentPlayerModel;
            playerModel?.gameObject.SetActive(false);
            blackBoard.PlayerData.playerSerializeData.skinIndex = index;
            playerModel = blackBoard.PlayerModels[index];
            playerModel.gameObject.SetActive(true);
            blackBoard.CurrentPlayerModel = playerModel;
            blackBoard.Animancer.Animator.avatar = playerModel.animator.avatar;
            if (blackBoard.CurrentAnimancerState != null)
                blackBoard.Animancer.Play(blackBoard.CurrentAnimancerState);
        }
    }
}