using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(ZipAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(ZipAction)), order = 0)]
    public class ZipAction : LocalmotionAction{
        [SerializeField] PlayerAnimTransition _zipTransition;
        private GameObject _displayZipPoint;
        private Vector3 _zipPoint;
        private bool _isZip;

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _displayZipPoint = playerController.DisplayZipPoint;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            //base.Enter();
            _state = _displayContainer.PlayAnimation(_zipTransition.startAnimation);
            _state.Events.OnEnd += CompleteZip;
            _zipPoint = new Vector3(_displayZipPoint.transform.position.x, _displayZipPoint.transform.position.y, _displayZipPoint.transform.position.z);
            _speed = 20;
            _isZip = false;
        }

        public override void Update()
        {
            if(!_isZip){
                return;
            }
            base.Update();
            if(Vector3.Distance(_playerController.transform.position,_zipPoint) < 0.1f){
                EndZip();
            }
        }

        public override void LateUpdate()
        {
            if(!_isZip){
                return;
            }
            base.LateUpdate();
            _moveDirection = (_zipPoint - _playerController.PlayerDisplay.position);
            
            //_playerController.transform.position += _moveDirection.normalized * _speed * Time.deltaTime;
            //MoveInAir();
        }

        public void CompleteZip(){
            _displayContainer.PlayAnimation(_zipTransition.keepAnimation);
            _isZip = true;
            _playerController.transform.DOMove(_zipPoint, Vector3.Distance(_zipPoint, _playerController.transform.position) / _speed)
                .SetEase(Ease.OutQuart)
                .OnComplete(EndZip);
            Debug.DrawRay(_playerController.PlayerDisplay.position, _moveDirection, Color.red, 10);
            _rotateDirection = Vector3.Cross(_moveDirection.normalized, _playerController.PlayerDisplay.right);
            Rotate();
        }

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            EndZip();
        }

        private void EndZip(){
            _playerController.transform.DOKill();
            _state.Events.OnEnd -= CompleteZip;
            _state = _displayContainer.PlayAnimation(_zipTransition.endAnimation);
            _state.Events.OnEnd += () => ChangeAction(ActionEnum.Idle);
        }

        private void ChangeAction(ActionEnum actionEnum){
            _state.Events.OnEnd = null;
            _stateContainer.ChangeAction(actionEnum);
        }

        public override bool Exit(ActionEnum actionEnum)
        {
            _state.Events.OnEnd -= CompleteZip;
            return base.Exit(actionEnum);
        }
    }
}