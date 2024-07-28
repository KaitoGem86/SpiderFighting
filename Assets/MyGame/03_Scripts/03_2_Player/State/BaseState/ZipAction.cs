using Core.GamePlay.Support;
using DG.Tweening;
using EasyCharacterMovement;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(ZipAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(ZipAction)), order = 0)]
    public class ZipAction : LocalmotionAction
    {
        private DisplayZipPoint _displayZipPoint;
        private Vector3 _zipPoint;
        private bool _isZip;
        private bool _jump;
        private bool _zipToPoint;
        private Transform _leftHand;
        private Transform _rightHand;
        private bool _isStartShootSilk = false;
        private int frame = 0;
        private int maxFrame = 5;
        private LineRenderer _left;
        private LineRenderer _right;

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _displayZipPoint = playerController.DisplayZipPoint;
            _left = playerController.LeftLine;
            _right = playerController.RightLine;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _playerController.CharacterMovement.rigidbody.useGravity = false;
            _playerController.CharacterMovement.rigidbody.isKinematic = true;
            _leftHand = _playerController.leftHand;
            _rightHand = _playerController.rightHand;
            if (_displayZipPoint.gameObject.activeSelf == false)
            {
                if (beforeAction == ActionEnum.Climbing)
                {
                    _zipPoint = _playerController.PlayerDisplay.transform.position + _playerController.PlayerDisplay.transform.up * 15;
                }
                else
                {
                    _zipPoint = _playerController.PlayerDisplay.transform.position + _playerController.PlayerDisplay.transform.forward * 25;
                }
                _zipToPoint = false;
            }
            else
            {
                _zipPoint = new Vector3(_displayZipPoint.TargetZipPoint.x, _displayZipPoint.TargetZipPoint.y, _displayZipPoint.TargetZipPoint.z);
                _zipToPoint = true;
            }

            frame = 0;
            _speed = 120;
            _isZip = false;
            _moveDirection = _zipPoint - _playerController.PlayerDisplay.transform.position;
            _rotateDirection = _moveDirection.normalized;
            _isStartShootSilk = false;
        }

        public override void Update()
        {
            base.Update();
            float distance = Vector3.Distance(_zipPoint, _playerController.transform.position);
            if (InputManager.instance.jump)
            {
                _jump = true;
            }
            else
            {
                if (distance > 10f)
                    _jump = false;
            }
            _moveDirection = _zipPoint - _playerController.transform.position;
        }

        public override void LateUpdate()
        {
            Rotate();
            if (_isZip)
            {
                ShootShilkPefFrame(frame, maxFrame);
                frame++;
                Move();
                return;
            }
        }

        protected override void Move()
        {
            var targetPosition = _playerController.transform.position + _moveDirection * 15 * Time.deltaTime;
            if (Vector3.Distance(targetPosition, _zipPoint) < 0.1f)
            {
                EndZip();
                return;
            }
            _playerController.CharacterMovement.rigidbody.Move( targetPosition, _playerController.transform.rotation);
        }

        protected override void Rotate()
        {
            base.Rotate();
        }

        public override void KeepAction()
        {
            base.KeepAction();
            float time = Vector3.Distance(_zipPoint, _playerController.PlayerDisplay.transform.position) / _speed;
            //_displayZipPoint.ZipToPoint(_playerController.transform, time).OnComplete(EndAction);
            //_playerController.transform.DOMove(_zipPoint, time).OnComplete(EndAction);
            _isZip = true;
            _moveDirection = _zipPoint - _playerController.transform.position;
            _rotateDirection = _moveDirection.normalized;
            Rotate();
        }

        public override void ExitAction()
        {
            EndZip();
        }

        private void EndZip()
        {
           //_playerController.transform.DOKill();
            _playerController.transform.position = _zipPoint;
            if (_zipToPoint)
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
            else
            {
                ChangeAction(ActionEnum.Dive);
            }
        }

        private void ChangeAction(ActionEnum actionEnum)
        {
            _stateContainer.ChangeAction(actionEnum);
        }

        public override bool Exit(ActionEnum actionEnum)
        {
            _left.SetPositions(new Vector3[] { _leftHand.position, _leftHand.position });
            _right.SetPositions(new Vector3[] { _rightHand.position, _rightHand.position });
            return base.Exit(actionEnum);
        }

        public void StartShootSilk()
        {
            _isStartShootSilk = true;
        }

        private void ShootShilkPefFrame(int frame, int maxFrame)
        {
            if (!_isStartShootSilk) return;
            Debug.DrawRay(_leftHand.position, _zipPoint - _leftHand.position, Color.red);
            Debug.DrawRay(_rightHand.position, _zipPoint - _rightHand.position, Color.red);
            _left.SetPositions(new Vector3[] { _leftHand.position, Vector3.Lerp(_leftHand.position, _zipPoint, 1) });
            _right.SetPositions(new Vector3[] { _rightHand.position, Vector3.Lerp(_rightHand.position, _zipPoint, 1) });
        }
    }
}