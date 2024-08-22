using UnityEngine;
using Animancer;
using DG.Tweening;

namespace Core.GamePlay.MyPlayer
{
    public class ZipState : BasePlayerState<ClipTransitionSequence>
    {
        private Vector3 _zipPoint;
        private Vector3 _forward;
        private Vector3 _upward;
        private bool _isChangeState;

        public override void EnterState()
        {
            base.EnterState();
            _isChangeState = false;
            _zipPoint = _blackBoard.CameraFindZipPoint.zipPoint;
            _forward = _zipPoint - _fsm.transform.position;
            _forward = _forward.normalized;
            Vector3 temp = Vector3.Cross(_forward, Vector3.up);
            _upward = Vector3.Cross(temp, _forward).normalized;
            _blackBoard.rig.DORotate(Quaternion.LookRotation(_forward, _upward).eulerAngles, 0.05f);
        }

        public void ShootSilk()
        {
            _blackBoard.rightSilk.Init();
            _blackBoard.leftSilk.Init();
            _blackBoard.rightSilk.ShootSilkToTarget(_blackBoard.CurrentPlayerModel.rightHand, _zipPoint, 0.1f);
            _blackBoard.leftSilk.ShootSilkToTarget(_blackBoard.CurrentPlayerModel.leftHand, _zipPoint, 0.1f);
        }

        public void MoveToZipPoint()
        {
            _blackBoard.rig.DOMove(_zipPoint, 0.65f).SetEase(Ease.OutSine)
                .OnComplete(
                    () => {
                        _blackBoard.rightSilk.UnUseSilk();
                        _blackBoard.leftSilk.UnUseSilk();
                        _forward.y = 0;
                        _blackBoard.rig.rotation = Quaternion.LookRotation(_forward, Vector3.up);
                        _blackBoard.rig.position = _zipPoint;
                        _blackBoard.rig.velocity = Vector3.zero;
                        _blackBoard.rig.angularVelocity = Vector3.zero;
                        _isChangeState = true;
                    }
                );
        }

        public void CompleteGroundZip()
        {
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }

        public void Launch(){
            if(_isChangeState){
                _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Launch);
            }
        }
    }
}