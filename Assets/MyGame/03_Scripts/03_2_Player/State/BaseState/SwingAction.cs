using EasyCharacterMovement;
using SFRemastered.InputSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(SwingAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(SwingAction)), order = 0)]
    public class SwingAction : InAirAction
    {
        [SerializeField] private float _angular;

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

        private float _t;

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _holdPivot = _playerController.HoldPivot;
            _handToUse = 1;
            _lineRenderer = _playerController.Line;
            rb = _playerController.swingPivot;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _velocity = _playerController.GetVelocity();
            _playerController.SetMovementMode(MovementMode.None);
            _playerController.CharacterMovement.rigidbody.isKinematic = false;
            _playerController.CharacterMovement.rigidbody.useGravity = true;
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _playerController.CharacterMovement.rigidbody.velocity = _velocity;
            FindPivot();
            InitSwing();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _playerController.PlayerDisplay.transform.up = Vector3.up;
            Destroy(_springJoint);
            _playerController.SetMovementMode(MovementMode.Walking);
            _playerController.CharacterMovement.rigidbody.isKinematic = true;
            _playerController.CharacterMovement.rigidbody.useGravity = false;
            return base.Exit(actionAfter);
        }

        private void FindPivot()
        {
            var tmp = InputManager.instance.move;
            var forward = _playerController.CameraTransform.forward * tmp.y;
            forward.y = 0;
            var right = _playerController.CameraTransform.right;
            //right.y = 0;
            var tmpDirection = forward + right;
            var tempFindDirection = new Vector3(tmpDirection.x, tmpDirection.magnitude, tmpDirection.z);
            // if (Physics.Raycast(_holdPivot.position + _playerController.PlayerDisplay.right * 5 * _handToUse, tempFindDirection, out RaycastHit hit, 100f, 6))
            //     _pivot = hit.point;
            // if (Vector3.Distance(_playerController.transform.position, _pivot) < 20)
            // {
            //     _pivot = _playerController.transform.position + (_pivot - _playerController.transform.position).normalized * 20;
            // }
            // else
            {
//                _pivot = _playerController.transform.position + _playerController.PlayerDisplay.up * 20 + _playerController.PlayerDisplay.right * 10 * _handToUse + _playerController.PlayerDisplay.forward * 20;
                _pivot = _playerController.transform.position + forward.normalized * 10 + right.normalized * 10 * _handToUse + Vector3.up * 20;
            }
            restLength = Vector3.Distance(_holdPivot.position, _pivot);
            _targetReslength = restLength * 0.8f;
            _handToUse = -_handToUse;

            rb.transform.position = _pivot;
        }

        public override void Update()
        {
            if (_playerController.CharacterMovement.velocity.magnitude > 35 && (Vector3.Angle(_playerController.GetVelocity(), Vector3.down) < 15))
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _stateContainer.ChangeAction(ActionEnum.Jumping);
                return;
            }
            _lineRenderer.SetPositions(new Vector3[] { _holdPivot.position, rb.transform.position });
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            GetInput();
            _playerController.CharacterMovement.rigidbody.AddForce(_moveDirection * 10, ForceMode.Force);
            RotateCharacterFlowVelocity();
        }

        public override void LateUpdate()
        {
            
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

        private void RotateCharacterFlowVelocity(){
            var velocity = _playerController.CharacterMovement.rigidbody.velocity;
            var targetRotation = Quaternion.LookRotation(velocity);
            _playerController.PlayerDisplay.transform.rotation = Quaternion.Slerp(_playerController.PlayerDisplay.transform.rotation, targetRotation, Time.deltaTime * 2 );
        }

        protected override void EndStateToClimb()
        {
            Debug.Log("EndStateToClimb");
            _playerController.SetVelocity(Vector3.zero);
        }
    }
}