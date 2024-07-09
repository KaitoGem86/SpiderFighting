using EasyCharacterMovement;
using SFRemastered.InputSystem;
using UnityEngine;

namespace Core.GamePlay.Player
{
    [CreateAssetMenu(fileName = nameof(SwingAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(SwingAction)), order = 0)]
    public class SwingAction : BasePlayerAction
    {
        [SerializeField] private float _angular;

        public Transform connectedBody;   // The Rigidbody to which the spring is connected
        public float springForce = 5; // The spring constant (k)
        public float damperForce = 5.0f;  // The damping coefficient (c)
        public float restLength = 1.0f;   // The rest length of the spring
        public float speed = 10f;       // Speed for manual control

        private Rigidbody rb;

        private LineRenderer _lineRenderer;
        private Transform _holdPivot;
        private Vector3 _pivot;
        private int _handToUse;
        private Vector3 _swingDirection;
        private Vector3 _remainingDirection;
        private Vector3 _lastDirection;
        private Vector3 _velocity;

        private float _mechanicalEnergy;

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
            _velocity = _playerController.CharacterMovement.rigidbody.velocity;
            _playerController.CharacterMovement.rigidbody.useGravity = true;
            _playerController.CharacterMovement.rigidbody.isKinematic = false;
            _playerController.CharacterMovement.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _handToUse = (_handToUse + 1) % 2;
            _mechanicalEnergy = _playerController.gravity.y * _playerController.CharacterMovement.transform.position.y + 1 / 2 * _playerController.CharacterMovement.velocity.sqrMagnitude;
            _playerController.CharacterMovement.velocity = Vector3.zero;
            FindPivot();
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
            if (Physics.Raycast(_holdPivot.position + _playerController.PlayerDisplay.right * Random.Range(-5f, 5f), tempFindDirection, out RaycastHit hit, 100f))
                _pivot = hit.point;
            else
            {
                Debug.Log("No Hit");
            }
            restLength = Vector3.Distance(_holdPivot.position, _pivot);
        }

        public override void Update()
        {
            base.Update();
            _lineRenderer.SetPositions(new Vector3[] { _holdPivot.position, _pivot });
        }

        public override void FixedUpdate()
        {
            base.LateUpdate();
            Swing();
        }

        private void Swing()
        {
            Vector3 input = InputManager.instance.move.normalized;
            var forward = _playerController.CameraTransform.forward * input.y;
            forward.y = 0;
            var right = _playerController.CameraTransform.right * input.x;
            right.y = 0;
            var tmpDirection = forward + right;
            _playerController.CharacterMovement.velocity += tmpDirection * speed * Time.fixedDeltaTime;

            Vector3 displacement = _playerController.CharacterMovement.transform.position - _pivot;
            float currentLength = displacement.magnitude;

            Vector3 direction = displacement.normalized;

            float swingMagnitude = springForce * (currentLength - restLength);

            Vector3 velocity = _playerController.CharacterMovement.velocity;
            float dampingMagnitude = damperForce * Vector3.Dot(velocity, direction);

            Vector3 force = (swingMagnitude + dampingMagnitude) * direction;

            velocity -= force * Time.deltaTime / _playerController.mass;
            _playerController.CharacterMovement.Move(velocity);

            Vector3 normalizedDirection = -displacement.normalized;

            // Calculate the rotation to align transform.up with the normalizedDirection
            Quaternion targetRotation = Quaternion.FromToRotation(_playerController.PlayerDisplay.up, normalizedDirection) * _playerController.PlayerDisplay.rotation;

            // Apply the rotation to transform.up
            _playerController.PlayerDisplay.rotation = targetRotation;
        }

        private void CalculateSwingDirection()
        {
            var tmp = _pivot - _playerController.CharacterMovement.transform.position;
            var verticalPlaneVel = Vector3.Cross(-tmp, _playerController.PlayerDisplay.transform.right);
            _velocity = Vector3.Cross(verticalPlaneVel, -_playerController.PlayerDisplay.transform.right);
            var h = _playerController.CharacterMovement.transform.position.y;
            Debug.Log(_mechanicalEnergy - _playerController.gravity.y * h);
            var v = Mathf.Sqrt(2 * Mathf.Abs(_mechanicalEnergy - _playerController.gravity.y * h));
            _velocity = _velocity.normalized * v;
        }
    }
}