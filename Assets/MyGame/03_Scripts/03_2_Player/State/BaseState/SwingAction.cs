using SFRemastered.InputSystem;
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

        private float _t;

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _holdPivot = _playerController.HoldPivot;
            _handToUse = 1;
            _lineRenderer = _playerController.Line;
        }

        public override void Enter(ActionEnum beforeAction)
        {
            base.Enter(beforeAction);
            _velocity = _playerController.CharacterMovement.rigidbody.velocity;
            _playerController.CharacterMovement.velocity = Vector3.zero;
            FindPivot();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _playerController.PlayerDisplay.transform.up = Vector3.up;
            return base.Exit(actionAfter);
        }

        private void FindPivot()
        {
            var tmp = InputManager.instance.move;
            var forward = _playerController.PlayerDisplay.forward * tmp.y;
            forward.y = 0;
            var right = _playerController.PlayerDisplay.right * tmp.x;
            right.y = 0;
            var tmpDirection = forward + right;
            var tempFindDirection = new Vector3(tmpDirection.x, tmpDirection.magnitude, tmpDirection.z);
            Debug.DrawRay(_holdPivot.position, tempFindDirection * 100, Color.red, 1000f);
            if (Physics.Raycast(_holdPivot.position + _playerController.PlayerDisplay.right * 5 * _handToUse, tempFindDirection, out RaycastHit hit, 100f))
                _pivot = hit.point;
            if (Vector3.Distance(_playerController.transform.position, _pivot) < 20)
            {
                _pivot = _playerController.transform.position + (_pivot - _playerController.transform.position).normalized * 20;
            }
            else
            {
                _pivot = _playerController.transform.position + _playerController.PlayerDisplay.up * 20 + _playerController.PlayerDisplay.right * 10 * _handToUse + _playerController.PlayerDisplay.forward * 20;
            }
            restLength = Vector3.Distance(_holdPivot.position, _pivot);
            _targetReslength = restLength * 0.8f;
            _t = 2 * Mathf.PI * Mathf.Sqrt(restLength / -_playerController.gravity.y) * 2 / 3;
            _handToUse = -_handToUse;
        }

        public override void Update()
        {
            _playerController.PlaneToSwing.transform.position = _playerController.transform.position + Vector3.up * 20;
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
            _t -= Time.deltaTime;
            base.Update();
            _lineRenderer.SetPositions(new Vector3[] { _holdPivot.position, _pivot });
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Swing();
        }

        public override void LateUpdate()
        {
        }

        private void Swing()
        {
            Vector3 displacement = _playerController.CharacterMovement.transform.position - _pivot;
            float currentLength = displacement.magnitude;

            Vector3 input = InputManager.instance.move.normalized;
            var forward = _playerController.CameraTransform.forward * input.y;
            forward.y = 0;
            var right = _playerController.CameraTransform.right * input.x;
            right.y = 0;
            var tmpDirection = forward + right;
            // if(input.magnitude < 0.1f){
            //     tmpDirection = _playerController.PlayerDisplay.forward;
            // }
            _playerController.CharacterMovement.velocity += (tmpDirection * speed ) * Time.fixedDeltaTime;

            Vector3 direction = displacement.normalized;

            float swingMagnitude = springForce * (currentLength - restLength);

            Vector3 velocity = _playerController.CharacterMovement.velocity;

            float dampingMagnitude = damperForce * Vector3.Dot(velocity, direction);

            Vector3 force = (swingMagnitude + dampingMagnitude) * direction;

            Debug.DrawRay(_playerController.CharacterMovement.transform.position, -force * 100, Color.red, 1000f);
            _playerController.AddForce(-force);

            Vector3 normalizedDirection = -displacement.normalized;

            // Calculate the rotation to align transform.up with the normalizedDirection
            Quaternion targetRotation = Quaternion.FromToRotation(_playerController.PlayerDisplay.up, normalizedDirection) * _playerController.PlayerDisplay.rotation;
            // var tmp = Vector3.Cross(normalizedDirection, _playerController.PlayerDisplay.forward);
            // Vector3 tmp2 = Vector3.Cross(tmp, normalizedDirection);
            
            // Quaternion targetRotation = Quaternion.LookRotation(tmp2, _playerController.transform.up);
            //var tmp = Quaternion.LookRotation(velocity);

            // Apply the rotation to transform.up
            _playerController.PlayerDisplay.rotation = targetRotation;
        }

        protected override void EndStateToClimb()
        {
            Debug.Log("EndStateToClimb");
            _playerController.SetVelocity(Vector3.zero);
        }
    }
}