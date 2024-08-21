using Animancer;
using Core.GamePlay.Support;
using DamageNumbersPro;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Mission.Protected
{
    public class NeedProtectedNPC : FSM<ProtectedNPCBlackBoard>, IHitted
    {
        [SerializeField] protected HPBarController _hpBarController;
        [SerializeField] protected DamageNumber _dameNumber;
        [SerializeField] protected Canvas _unitCanvas;

        protected override void OnEnable()
        {
            base.OnEnable();
            blackBoard.hp = 100;
            _hpBarController.SetHP(1);
        }

        public void HittedByPlayer(FSMState state)
        {
            blackBoard.hp -= 10;
            _hpBarController.SetHP(blackBoard.hp / 100);
            _dameNumber.Spawn(_unitCanvas.transform.position, 10 , _unitCanvas.transform);
            if (blackBoard.hp <= 0)
            {
                Debug.Log("NPC is dead");
                ChangeAction(FSMState.Dead);
            }
        }

        public void HittedBySpecialSkill(FSMState state, ClipTransitionSequence responseClip)
        {
            Debug.Log("Hitted by special skill");
        }

        public Transform TargetEnemy
        {
            get => transform;
        }

        public bool IsIgnore { get; set; }
        public bool IsPlayer { get => blackBoard.isPlayer; }
        public ClipTransitionSequence ResponseClip { get; set; }
    }
}