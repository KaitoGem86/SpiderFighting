using System.Collections;
using DG.Tweening.Plugins.Options;
using EasyCharacterMovement;
using MyTools.Event;
using SFRemastered.InputSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(SwingAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(SwingAction)), order = 0)]
    public class SwingAction : InAirAction
    {
        [SerializeField] private float _angular;
        [SerializeField] BoolSerializeEventListener _onSwing;

        public Transform connectedBody;   // The Rigidbody to which the spring is connected
        public float springForce = 5000; // The spring constant (k)
        public float damperForce = 5.0f;  // The damping coefficient (c)
        public float restLength = 1.0f;   // The rest length of the spring
        public float speed = 10f;       // Speed for manual control

        private Rigidbody rb;

        private LineRenderer _lineRenderer;
        private Transform _holdPivot;
        private Vector3 _pivot;
        private int _handToUse;
        private Vector3 _velocity;
        private float _targetReslength;
        private SpringJoint _springJoint;
        private Transform _leftHand;
        private Transform _rightHand;
        private int frame = 0;
        private int maxFrame = 10;
        private bool _isStartShootSilk = false;


        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _holdPivot = _playerController.HoldPivot;
            _handToUse = 1;
            _lineRenderer = _playerController.LeftLine;
            rb = _playerController.swingPivot;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            _leftHand = _playerController.leftHand;
            _rightHand = _playerController.rightHand;
            FindPivot();
            base.Enter(beforeAction);
            _onSwing.RegisterListener();
            _velocity = _playerController.GlobalVelocity;
            _playerController.SetMovementMode(MovementMode.None);
            _playerController.CharacterMovement.rigidbody.isKinematic = false;
            _playerController.CharacterMovement.rigidbody.useGravity = true;
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _playerController.CharacterMovement.rigidbody.velocity = _velocity;
            _isStartShootSilk = false;
            InitSwing();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _onSwing.UnregisterListener();
            _isStartShootSilk = false;
            _lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
            _playerController.PlayerDisplay.transform.up = Vector3.up;
            _playerController.GlobalVelocity = _playerController.CharacterMovement.rigidbody.velocity;
            Destroy(_springJoint);
            _playerController.SetMovementMode(MovementMode.Walking);
            _playerController.CharacterMovement.rigidbody.isKinematic = true;
            _playerController.CharacterMovement.rigidbody.useGravity = false;
            return base.Exit(actionAfter);
        }

        private void FindPivot()
        {
            var tmp = InputManager.instance.move;
            var forward = _playerController.CameraTransform.forward;
            forward.y = 0;
            var right = _playerController.CameraTransform.right;
            right.y = 0;
            var tmpDirection = forward + right;
            var tempFindDirection = new Vector3(tmpDirection.x, tmpDirection.magnitude, tmpDirection.z);
            if (_playerController.transform.position.y < 10)
            {
                _speed = 30;
                _pivot = _playerController.transform.position + forward.normalized * 10 + right.normalized * 10 * _handToUse + Vector3.up * 20;
            }
            else
            {
                _speed = 50;
                _pivot = _playerController.transform.position + forward.normalized * 20 + right.normalized * 10 * _handToUse + Vector3.up * 20;
            }
            restLength = Vector3.Distance(_holdPivot.position, _pivot);
            _targetReslength = restLength * 0.8f;
            _handToUse = -_handToUse;

            rb.transform.position = _pivot;
        }

        public override void Update()
        {
            if ((Vector3.Angle(_holdPivot.position - rb.transform.position, Vector3.down) > 87 && Vector3.Dot(_playerController.CharacterMovement.rigidbody.velocity, Vector3.up) > 0))
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            ShootShilkPefFrame(_handToUse == 1 ? _leftHand : _rightHand, frame, maxFrame);
            frame = Mathf.Min(frame + 1, maxFrame);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            GetInput();
            _playerController.CharacterMovement.rigidbody.AddForce(_moveDirection * _speed, ForceMode.Force);
            RotateCharacterFlowVelocity();
        }

        public override void LateUpdate()
        {

        }

        public void ChangeToJumping(bool isSwing){
            if(!isSwing){
                _stateContainer.ChangeAction(ActionEnum.Jumping);
            }
        }

        private void InitSwing()
        {
            _springJoint = _playerController.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedBody = rb;
            _springJoint.connectedAnchor = _holdPivot.localPosition;
            _springJoint.maxDistance = Vector3.Distance(_playerController.transform.position, _pivot);

            _springJoint.spring = 4.5f;
            _springJoint.damper = 1f;
            _springJoint.massScale = 4.5f;
        }

        private void RotateCharacterFlowVelocity()
        {
            var velocity = _playerController.CharacterMovement.rigidbody.velocity;
            var targetRotation = Quaternion.LookRotation(velocity);
            _playerController.PlayerDisplay.transform.rotation = Quaternion.Slerp(_playerController.PlayerDisplay.transform.rotation, targetRotation, Time.deltaTime * 2);
        }

        protected override void EndStateToClimb()
        {
            Debug.Log("EndStateToClimb");
        }

        protected override int GetTransition(ActionEnum actionBefore)
        {
            if (_handToUse == 1)
                return Random.Range(0, 9);
            else
            {
                return Random.Range(9, 18);
            }
        }

        public void ShootSpiderSilk()
        {
            _isStartShootSilk = true;
            frame = 0;
            maxFrame = 10;
        }

        private void ShootShilkPefFrame(Transform hand, int frame, int maxFrame)
        {
            if (!_isStartShootSilk) return;
            //Debug.Log("ShootShilkPefFrame");
            _lineRenderer.SetPositions(new Vector3[] { hand.position, hand.position + (_pivot - hand.position) .normalized * frame });
        }
    }
}