using Core.SystemGame;
using DG.Tweening;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(IdleAction)), order = 0)]
    public class IdleAction : LocalmotionAction
    {
        bool _isCanChangeAction = true;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter(ActionEnum beforeAction)
        {
            _playerController.GlobalVelocity = Vector3.zero;
            base.Enter(beforeAction);
            _onAttack?.RegisterListener();
            if (beforeAction == ActionEnum.Zip)
            {
                _isCanChangeAction = false;
            }  
        }  

        public override bool Exit(ActionEnum actionAfter)
        {
            _onAttack?.UnregisterListener();
            return base.Exit(actionAfter);
        }

        public override void Update()
        {
            if (!_isCanChangeAction)
            {
                return;
            }
            if (InputManager.instance.jump)
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            if (InputManager.instance.move.magnitude > 0.1f)
            {
                _stateContainer.ChangeAction(ActionEnum.Moving);
                return;
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            ReRotateCharacter();
        }
        public override void KeepAction()
        {
            base.KeepAction();
            _isCanChangeAction = true;
        }

        private void ReRotateCharacter()
        {
            var rotateDir = _playerController.PlayerDisplay.transform.forward;
            rotateDir.y = 0;
            var targetRotation = Quaternion.LookRotation(rotateDir);
            _playerController.PlayerDisplay.transform.rotation = Quaternion.Slerp(_playerController.PlayerDisplay.transform.rotation, targetRotation, Time.deltaTime * 10);
        }
    }
}