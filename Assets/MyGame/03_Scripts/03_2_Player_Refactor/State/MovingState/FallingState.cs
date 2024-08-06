using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class FallingState : InAirState<ClipTransition>
    {
        [SerializeField] private float _diveVelocityThreshold = 1f;
        public override void EnterState()
        {
            this.gameObject.SetActive(true);
        }

        public override void Update()
        {
            if (_fsm.blackBoard.Character.IsGrounded())
            {
                _fsm.ChangeAction(FSMState.Landing);
                return;
            }
            if (_fsm.blackBoard.GetVelocity.magnitude > _diveVelocityThreshold)
            {
                _fsm.ChangeAction(FSMState.Dive);
                return;
            }
        }
    }
}