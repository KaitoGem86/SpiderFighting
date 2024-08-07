using Animancer;
using Extensions.SystemGame.AIFSM;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class LandState : LocalmotionState<LinearMixerTransition>
    {
        [SerializeField] private float _landingVelocityThreshold = 1f;
        private bool _isCanChangeAction = false;

        public override void EnterState()
        {
            _fsm.blackBoard.Character.StopJumping();
            base.EnterState();
            var velocity = _fsm.blackBoard.Character.GetCharacterMovement().velocity.magnitude;
            GetInput();
            if (_moveDirection.magnitude < 0.1f)
            {
                _transition.State.Parameter = 0;
            }
            else if (velocity < _landingVelocityThreshold)
            {
                _transition.State.Parameter = 1;
            }
            else
            {
                _transition.State.Parameter = 2;
            }
        }

        public override void Update()
        {
            if (!_isCanChangeAction)
            {
                return;
            }
            if (InputManager.instance.move.magnitude > 0.1f)
            {
                _fsm.ChangeAction(FSMState.Moving);
                return;
            }
        }

        public void CanChangeToAction()
        {
            _isCanChangeAction = true;
        }

        public void CompleteLand()
        {
            _fsm.ChangeAction(FSMState.Idle);
        }
    }
}