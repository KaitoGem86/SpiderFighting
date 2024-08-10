using Animancer;
using EasyCharacterMovement;
using Extensions.SystemGame.AIFSM;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class MoveState : LocalmotionState<LinearMixerTransition>
    {
        private Transform _checkWallPivot;

        protected override void Awake()
        {
            base.Awake();
            _speed = _fsm.blackBoard.PlayerData.playerStatSO.GetGlobalStat(Data.Stat.Player.PlayerStat.Speed);
        }

        public override void EnterState()
        {
            base.EnterState();
            //_fsm.blackBoard.Character.maxWalkSpeed = _speed;
        }

        public override void Update()
        {
            if (!_fsm.blackBoard.Character.IsOnGround())
            {
                _fsm.ChangeAction(FSMState.FallingDown);
                return;
            }
            GetInput();
            if (_moveDirection.Equals(Vector3.zero))
            {
                OnDontMove();
            }
        }

        public void LateUpdate()
        {
            Move();
            //Rotate();
            _transition.State.Parameter = _moveDirection.magnitude * _speed;
        }

        protected virtual void OnDontMove()
        {
            _fsm.ChangeAction(FSMState.StopMoving);
        }

        public override void OnCollided(ref CollisionResult other)
        {
            if (_fsm.blackBoard.Character.GetCharacterMovement().groundCollider == null) return;
            if (other.collider.gameObject == _fsm.blackBoard.Character.GetCharacterMovement().groundCollider.gameObject) return;
            base.OnCollided(ref other);
            // if (Physics.Raycast(_checkWallPivot.position, _fsm.blackBoard.PlayerDisplay.forward, out var hit, _fsm.blackBoard.Character.GetRadius()))
            // {
            //     if (Vector3.Angle(hit.normal, Vector3.up) > _fsm.blackBoard.Character.GetCharacterMovement().slopeLimit)
            //     {
            //         //_stateContainer.SurfaceNormal = hit.normal;
            //         //_stateContainer.ChangeAction(ActionEnum.Climbing);
            //     }
            // }
        }
    }
}