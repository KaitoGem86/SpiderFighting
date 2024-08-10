using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class IdleState : ClipTransitionPlayerState
    {
        bool _isCanChangeAction = true;

        public override void EnterState()
        {
            _fsm.blackBoard.GlobalVelocity = Vector3.zero;
            base.EnterState();
        }

        public override void Update()
        {
            if (!_isCanChangeAction)
            {
                return;
            }
            if (InputManager.instance.jump)
            {
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Jumping);
                return;
            }
            if (InputManager.instance.move.magnitude > 0.1f)
            {
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Moving);
                return;
            }
            
        }

        public void LateUpdate()
        {
            //ReRotateCharacter();
        }

        // private void ReRotateCharacter()
        // {
        //     var rotateDir = _fsm.blackBoard.PlayerDisplay.transform.forward;
        //     rotateDir.y = 0;
        //     var targetRotation = Quaternion.LookRotation(rotateDir);
        //     _fsm.blackBoard.PlayerDisplay.transform.rotation = Quaternion.Slerp(_fsm.blackBoard.PlayerDisplay.transform.rotation, targetRotation, Time.deltaTime * 10);
        // }
    }
}