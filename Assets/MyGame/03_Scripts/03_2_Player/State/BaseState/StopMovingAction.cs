using Core.SystemGame;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(StopMovingAction), menuName = ("PlayerState/" + nameof(StopMovingAction)), order = 0)]
    public class StopMovingAction : LocalmotionAction{
        [SerializeField] private float _damping = 0f;
        private float _targetSpeed = 0;
        private Vector3 _remainMoveDirection = Vector3.zero;
        public override void Enter()
        {
            base.Enter();
            _speed = 5;
            _targetSpeed = 0;
            _remainMoveDirection = _playerController.PlayerDisplay.transform.forward * 0.3f;
        }

        public override void LateUpdate()
        {
            GetInput();
            if(InputManager.instance.move.magnitude > 0.1f){
                _stateContainer.ChangeAction(ActionEnum.Moving);
                return;
            }
            // if(InputSystem.Instance.IsJump){
            //     _stateContainer.ChangeAction(ActionEnum.Jumping);
            //     return;
            // }
            _speed = Mathf.Lerp(_speed, _targetSpeed, _damping * Time.deltaTime);
            if(_speed < 0.1f){
                _speed = _targetSpeed;
            }
            _moveDirection = _remainMoveDirection;
            //MoveInAir();
            base.LateUpdate();
            _remainMoveDirection = Vector3.Lerp(_remainMoveDirection, Vector3.zero, _damping * Time.deltaTime);
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public void CheckMoving(){
            if(InputManager.instance.move.magnitude > 0.1f){
                // if(InputSystem.Instance.IsSprint){
                //     _stateContainer.ChangeAction(ActionEnum.Sprinting);
                //     return;
                // }
                //else{
                    _stateContainer.ChangeAction(ActionEnum.Moving);
                    return;
                //}
            }
            else{
                _stateContainer.ChangeAction(ActionEnum.Idle);
                return;
            }
        }
    }
}