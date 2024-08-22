using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class FallingState : InAirState<ClipTransition>
    {
        [SerializeField] private float _diveVelocityThreshold = 1f;
        protected override void Awake(){
            base.Awake();
            _diveVelocityThreshold = _fsm.blackBoard.PlayerData.playerStatSO.GetGlobalStat(Data.Stat.Player.PlayerStat.DiveVelocityThreshold);
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void Update()
        {
            if (_fsm.blackBoard.Character.IsGrounded())
            {
                _fsm.ChangeAction(FSMState.Landing);
                return;
            }
            if (_fsm.blackBoard.GetVelocity.y < -_diveVelocityThreshold)
            {
                _fsm.ChangeAction(FSMState.Dive);
                return;
            }
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Rotate();
        }
    }
}