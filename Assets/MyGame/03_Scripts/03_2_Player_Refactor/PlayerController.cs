using Core.GamePlay.Support;
using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class PlayerController : FSM<PlayerBlackBoard>, IHitted
    {
        protected override void Awake()
        {
            base.Awake();
            currentStateType = FSMState.None;
            blackBoard.PlayerData.Init();
        }

        protected override void OnEnable()
        {
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

        protected void Update()
        {
            if (blackBoard.GetVelocity.magnitude > 60)
            {
                blackBoard.OnReachMaxSpeed.Raise(true);
            }
            else
            {
                blackBoard.OnReachMaxSpeed.Raise(false);
            }
        }

        public void HittedByPlayer(FSMState state)
        {
            if (currentStateType == FSMState.Dodge || currentStateType == FSMState.UltimateAttack)
            {
                Debug.Log("------------------- Miss -----------------");
                return;
            }
            var hp = blackBoard.PlayerData.localStats[Data.Stat.Player.PlayerStat.HP];
            var maxHP = blackBoard.PlayerData.playerStatSO.GetGlobalStat(Data.Stat.Player.PlayerStat.HP);
            hp -= 100;
            hp = Mathf.Max(0, hp);
            blackBoard.PlayerData.localStats[Data.Stat.Player.PlayerStat.HP] = hp;
            blackBoard.OnAttack.Raise(hp / maxHP);
        }

        public Transform TargetEnemy => transform;
        public bool IsIgnore { get; set; }
        public bool IsPlayer => true;
    }
}