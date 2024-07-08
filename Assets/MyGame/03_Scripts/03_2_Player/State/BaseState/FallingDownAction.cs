using Core.GamePlay.Player;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = nameof(BasePlayerAction), menuName = ("PlayerState/" + nameof(FallingDownAction)), order = 0)]
    public class FallingDownAction : LocalmotionAction
    {
        [SerializeField] private float _heightThreshHold = 2f;
        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
        }

        public override void Enter()
        {
            var tmp = RaycastCheckGround();
            if(tmp.Item2 > _heightThreshHold)
                _priority = PriorityEnum.Critical;
            else
                _priority = PriorityEnum.High;
            if(_stateContainer.CurrentInteractionAction == ActionEnum.None || _priority != PriorityEnum.Critical)
                base.Enter();
            _speed = 5;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            GetInput();
            if (RaycastCheckGround().Item1)
            {
                if (Vector3.Angle(_surfaceNormal, Vector3.up) > 45f)
                {
                    if (_rotateDirection != Vector3.zero)
                        Rotate();
                    if(_playerController.CharacterMovement.isGrounded)
                    {
                        if(Physics.Raycast(_playerController.CharacterMovement.transform.position, Vector3.down, out RaycastHit hit)){
                            if(Vector3.Angle(hit.normal, Vector3.up) < 45f)
                                _stateContainer.ChangeAction(ActionEnum.Landing);
                        }
                    }
                    return;
                }
            }
            if (RaycastCheckGround().Item2 < 0.6f)
            {
                if (Vector3.Angle(_surfaceNormal, Vector3.up) < 45f)
                {
                    _stateContainer.ChangeAction(ActionEnum.Landing);
                    return;
                }
            }
            if (_rotateDirection != Vector3.zero)
                Rotate();
            MoveInAir();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            if (actionAfter != ActionEnum.Landing && actionAfter != ActionEnum.Sliding) return false;
            return base.Exit(actionAfter);
        }
    }
}