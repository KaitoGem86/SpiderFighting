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
            _playerController.SetMovementMode(MovementMode.None);
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
            if (Vector3.Distance(_zipPoint, _playerController.PlayerDisplay.transform.position) < 0.2f)
            {
                EndAction();
            }
            var distance = Vector3.Distance(_zipPoint, _playerController.PlayerDisplay.transform.position);
            _moveDirection = (_zipPoint - _playerController.PlayerDisplay.transform.position) * (distance > 1 ? 5 : 0.3f);
        }

        public override void LateUpdate()
        {
            Rotate();
            if (!_isZip) return;
            base.LateUpdate();
            Move();
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
            _playerController.SetMovementMode(MovementMode.Flying);
            _isZip = true;
            _moveDirection = _zipPoint - _playerController.PlayerDisplay.transform.position;
            _rotateDirection = _moveDirection.normalized;
            Rotate();
        }

        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            var surfaceNormal = collision.contacts[0].normal;
            var angle = Vector3.Angle(Vector3.up, surfaceNormal);
            if (angle > 45)
            {
                RaycastHit hit;
                if (Physics.Raycast(_playerController.transform.position, _moveDirection, out hit, 100))
                {
                    if (hit.distance < 0.8f)
                    {
                        _surfaceNormal = hit.normal;
                        _stateContainer.SurfaceNormal = _surfaceNormal;
                        _stateContainer.ChangeAction(ActionEnum.Climbing);
                        return;
                    }
                }
            }
            EndAction();
        }

        protected override void ExitAction()
        {
            EndZip();
        }

        private void EndZip()
        {
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