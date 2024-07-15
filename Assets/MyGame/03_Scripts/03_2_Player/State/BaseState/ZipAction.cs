using DG.Tweening;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(ZipAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(ZipAction)), order = 0)]
    public class ZipAction : LocalmotionAction{
        private GameObject _displayZipPoint;
        private Vector3 _zipPoint;
        private bool _isZip;
        private bool _jump;

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _displayZipPoint = playerController.DisplayZipPoint;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _zipPoint = new Vector3(_displayZipPoint.transform.position.x, _displayZipPoint.transform.position.y, _displayZipPoint.transform.position.z);
            _speed = 20;
            _isZip = false;
        }

        public override void LateUpdate()
        {
            if(!_isZip){
                return;
            }
            base.LateUpdate();
            if(InputManager.instance.jump){
                _jump = true;
            }
            else{
                if(Vector3.Distance(_zipPoint, _playerController.transform.position) > 1f)
                    _jump = false;
            }
        }

        public override void KeepAction()
        {
            base.KeepAction();
            CompleteZip();
        }

        public void CompleteZip(){
            _isZip = true;
            _playerController.transform.DOMove(_zipPoint, Vector3.Distance(_zipPoint, _playerController.transform.position) / _speed)
                .SetEase(Ease.OutCubic)
                .OnComplete(EndAction);
            Debug.DrawRay(_playerController.PlayerDisplay.position, _moveDirection, Color.red, 10);
            _rotateDirection = Vector3.Cross(_moveDirection.normalized, _playerController.PlayerDisplay.right);
            Rotate();
        }

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            EndZip();
        }

        protected override void ExitAction()
        {
            EndZip();
        }

        private void EndZip(){
            _playerController.transform.DOKill();
            if(_jump){
                ChangeAction(ActionEnum.Jumping);
            }
            else{
                InputManager.instance.jump = false;
                ChangeAction(ActionEnum.Idle);
            }
        }

        private void ChangeAction(ActionEnum actionEnum){
            _stateContainer.ChangeAction(actionEnum);
        }

        public override bool Exit(ActionEnum actionEnum)
        {
            return base.Exit(actionEnum);
        }
    }
}