using Animancer;
using Core.GamePlay.Support;
using DG.Tweening;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class SwingState : InAirState<ClipTransitionSequence>
    {
        [SerializeField] private ShootSilk _shootSilk;
        private Rigidbody _rb;
        private Transform _holdPivot;
        private Vector3 _pivot;
        private int _handToUse;
        private SpringJoint _springJoint;
        private Transform _leftHand;
        private Transform _rightHand;
        private bool _isStartSwing = false;

        protected override void Awake()
        {
            base.Awake();
            _holdPivot = _fsm.blackBoard.HoldPivot;
            _handToUse = 1;
            _rb = _fsm.blackBoard.SwingPivot;
        }

        public override void EnterState()
        {
            _leftHand = _fsm.blackBoard.CurrentPlayerModel.leftHand;
            _rightHand = _fsm.blackBoard.CurrentPlayerModel.rightHand;
            _isStartSwing = false;
            base.EnterState();
        }

        public override void ExitState()
        {
            if (_springJoint != null)
                _springJoint.maxDistance = float.MaxValue;
            _shootSilk.UnUseSilk();

            base.ExitState();
        }

        private void FindPivot()
        {
            var forward = _fsm.transform.forward;
            forward.y = 0;
            var right = _fsm.transform.right;
            right.y = 0;
            if (_fsm.transform.position.y < 10)
            {
                //_speed += _statManager.GetValue(Support.StatType.IncreaseSwingSpeed1).value;
                //_speed = Mathf.Clamp(_speed, 0, 30);
                _pivot = _fsm.transform.position + forward.normalized * 20 + right.normalized * 10 * _handToUse + Vector3.up * 15;
            }
            else
            {
                //_speed += _statManager.GetValue(Support.StatType.IncreaseSwingSpeed2).value;
                //_speed = Mathf.Clamp(_speed, 0, 80);
                _pivot = _fsm.transform.position + forward.normalized * 30 + right.normalized * 10 * _handToUse + Vector3.up * 20;
            }
            _handToUse = -_handToUse;
            _rb.transform.position = _pivot;
        }

        public override void Update()
        {
            if (!_isStartSwing)
                return;
            if ((Vector3.Angle(_holdPivot.position - _rb.transform.position, Vector3.down) > 87 && Vector3.Dot(_fsm.blackBoard.GetVelocity, Vector3.up) > 0))
            {
                _fsm.ChangeAction(FSMState.JumpFromSwing);
                return;
            }
            base.Update();
        }

        public override void FixedUpdate()
        {
            _fsm.blackBoard.Character.GetCharacterMovement().rigidbody.AddForce(_moveDirection * _speed, ForceMode.Force);
            Rotate();
        }

        public void ChangeToJumping(bool isSwing)
        {
            if (!isSwing)
            {
                _fsm.ChangeAction(FSMState.JumpFromSwing);
            }
        }

        public void ShootShilk()
        {
            FindPivot();
            _shootSilk.Init();
            _shootSilk.ShootSilkToTarget(_handToUse == 1 ? _leftHand : _rightHand, _rb.transform.position, 0.3f)
                .OnComplete(InitSwing);
        }

        private void InitSwing()
        {
            _isStartSwing = true;
            if (_springJoint == null)
            {
                _springJoint = _fsm.gameObject.AddComponent<SpringJoint>();
            }
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedBody = _rb;
            _springJoint.connectedAnchor = _holdPivot.localPosition;
            _springJoint.maxDistance = Vector3.Distance(_holdPivot.transform.position, _pivot);

            _springJoint.spring = 4.5f;
            _springJoint.damper = 1f;
            _springJoint.massScale = 4.5f;
        }

        protected override void Rotate()
        {
            Quaternion rotation = Quaternion.LookRotation(_fsm.transform.forward, (_rb.transform.position - _holdPivot.position).normalized);
            _fsm.transform.rotation = Quaternion.Lerp(_fsm.transform.rotation, rotation, 0.2f * 10 * Time.fixedDeltaTime);
            _fsm.blackBoard.Character.RotateTowardsWithSlerp(_fsm.blackBoard.Character.GetCharacterMovement().rigidbody.velocity.normalized, false);
        }


        protected override int GetIndexTransition()
        {
            if (_handToUse == -1)
                return Random.Range(0, _transitions.Length / 2);
            else
            {
                return Random.Range(_transitions.Length / 2, _transitions.Length);
            }
        }
    }
}