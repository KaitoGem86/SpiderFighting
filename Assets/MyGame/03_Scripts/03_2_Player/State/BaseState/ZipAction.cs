using DG.Tweening;
using EasyCharacterMovement;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(ZipAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(ZipAction)), order = 0)]
    public class ZipAction : LocalmotionAction
    {
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
            _playerController.SetMovementMode(MovementMode.Flying);
            _playerController.SetVelocity(Vector3.zero);
            _zipPoint = new Vector3(_displayZipPoint.transform.position.x, _displayZipPoint.transform.position.y, _displayZipPoint.transform.position.z);
            _speed = 40;
            _isZip = false;
        }

        public override void Update()
        {
            base.Update();
            if (InputManager.instance.jump)
            {
                _jump = true;
            }
            else
            {
                if (Vector3.Distance(_zipPoint, _playerController.transform.position) > 1f)
                    _jump = false;
            }
            if(Vector3.Distance(_zipPoint, _playerController.transform.position) < 0.1f)
            {
                EndAction();
            }
            _moveDirection = _zipPoint - _playerController.PlayerDisplay.transform.position;
        }

        public override void LateUpdate()
        {
            if(!_isZip) return;
            base.LateUpdate();
            Move();
            Rotate();
        }

        protected override void Move()
        {
            _playerController.SetVelocity(_moveDirection.normalized * _speed);
        }

        protected override void Rotate()
        {
            Debug.DrawRay(_playerController.transform.position, _rotateDirection, Color.red, 10f);
            base.Rotate();
        }

        public override void KeepAction()
        {
            base.KeepAction();
            _isZip = true;
            _moveDirection = _zipPoint - _playerController.PlayerDisplay.transform.position;
            _rotateDirection = _moveDirection.normalized;
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

        private void EndZip()
        {
            _playerController.transform.DOKill();
            if (_jump)
            {
                ChangeAction(ActionEnum.Jumping);
            }
            else
            {
                InputManager.instance.jump = false;
                ChangeAction(ActionEnum.Idle);
            }
        }

        private void ChangeAction(ActionEnum actionEnum)
        {
            _stateContainer.ChangeAction(actionEnum);
        }

        public override bool Exit(ActionEnum actionEnum)
        {
            _playerController.SetVelocity(Vector3.zero);
            _playerController.SetMovementMode(MovementMode.Walking);
            return base.Exit(actionEnum);
        }
    }
}