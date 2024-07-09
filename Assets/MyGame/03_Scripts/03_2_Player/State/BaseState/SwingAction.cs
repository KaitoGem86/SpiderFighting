using EasyCharacterMovement;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(SwingAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(SwingAction)), order = 0)]
    public class SwingAction : BasePlayerAction{
        [SerializeField] private float _zValue;
        
        private LineRenderer _lineRenderer;
        private Transform _holdPivot;
        private Vector3 _pivot;
        private int _handToUse;
        private Vector3 _swingDirection;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _holdPivot = _playerController.HoldPivot;
            _handToUse = 0;
            _lineRenderer = _playerController.Line;
        }

        public override void Enter()
        {
            base.Enter();
            _playerController.CharacterMovement.rigidbody.useGravity = false;
            _playerController.CharacterMovement.rigidbody.isKinematic = true;
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _handToUse = (_handToUse + 1) % 2;
            FindPivot();
        }

        private void FindPivot(){
            var tmp = _playerController.PlayerDisplay.transform.forward;
            var tempFindDirection = new Vector3(tmp.x, 1, tmp.z);
            if(Physics.Raycast(_holdPivot.position, tempFindDirection, out RaycastHit hit, 100f))
                _pivot = hit.point;
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            Swing();
        }

        private void Swing(){
            CalculateSwingDirection();
            _lineRenderer.SetPositions(new Vector3[]{_holdPivot.position, _pivot});
            _playerController.CharacterMovement.Move(_swingDirection);
        }

        private void CalculateSwingDirection(){
            var tmp = _pivot - _holdPivot.position;
            _swingDirection = Vector3.Cross(tmp, -_playerController.transform.right).normalized * 10;
        }
    }
}