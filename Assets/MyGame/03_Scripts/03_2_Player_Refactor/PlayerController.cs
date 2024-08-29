using System.Collections;
using Animancer;
using Core.GamePlay.Mission.Protected;
using Core.GamePlay.Support;
using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using JetBrains.Annotations;
using MyTools.ScreenSystem;
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
            StartCoroutine(TempFixBugs());
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

        public void OnRetryMission()
        {
            ChangeAction(_startState);
            blackBoard.rig.position = Vector3.zero;
            blackBoard.rig.rotation = Quaternion.identity;
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

        public void HittedByPlayer(FSMState state, float damage = 10)
        {
            if (currentStateType == FSMState.Dodge || currentStateType == FSMState.UltimateAttack)
            {
                Debug.Log("------------------- Miss -----------------");
                return;
            }
            blackBoard.AttackCount = 0;
            blackBoard.ResetTime();
            blackBoard.OnShowHitCounter.Raise(-1);
            var hp = blackBoard.PlayerData.localStats[Data.Stat.Player.PlayerStat.HP];
            var maxHP = blackBoard.PlayerData.playerStatSO.GetGlobalStat(Data.Stat.Player.PlayerStat.HP);
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                blackBoard.PlayerData.localStats[Data.Stat.Player.PlayerStat.HP] = hp;
                blackBoard.OnAttack.Raise(hp / maxHP);
                ChangeAction(FSMState.Dead);
                return;
            }
            hp = Mathf.Max(0, hp);
            blackBoard.PlayerData.localStats[Data.Stat.Player.PlayerStat.HP] = hp;
            blackBoard.OnAttack.Raise(hp / maxHP);
            if (state == FSMState.ResponeForSpecialSkill)
            {
                ChangeAction(FSMState.ResponeForSpecialSkill);
                return;
            }
            if (state == FSMState.Hit)
            {
                ChangeAction(FSMState.Hit);
                return;
            }
        }

        public void HittedBySpecialSkill(FSMState state, ClipTransitionSequence responseClip, float damage = 10)
        {
            blackBoard.AttackCount = 0;
            blackBoard.ResetTime();
            blackBoard.OnShowHitCounter.Raise(-1);
            blackBoard.PlayerController.ResponseClip = responseClip;
        }

        public void CollectReward()
        {
            blackBoard.CollectibleController?.OnCollect();
        }

        public Transform TargetEnemy => transform;
        public bool IsIgnore { get; set; }
        public bool IsPlayer => true;
        public ClipTransitionSequence ResponseClip { get; set; }

        private IEnumerator TempFixBugs(){
            ChangeAction(FSMState.Moving);
            yield return new WaitForSeconds(0.5f);
            ChangeAction(FSMState.Spawn);
        }
    }
}