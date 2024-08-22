using UnityEngine;
using Animancer;
using DG.Tweening;

namespace Core.GamePlay.MyPlayer{
    public class ZipState : BasePlayerState<ClipTransitionSequence>{
        private Vector3 _zipPoint;

        public override void EnterState()
        {
            base.EnterState();
            _zipPoint = _blackBoard.CameraFindZipPoint.zipPoint;
            _blackBoard.rig.DOLookAt(_zipPoint, 0.5f).OnComplete( () => { Debug.Log("Look at zip point"); });
        }

        public void ShootSilk(){

        }

        public void MoveToZipPoint(){
            Debug.DrawLine(_fsm.transform.position, _zipPoint, Color.red, 100f);
            _blackBoard.rig.DOMove(_zipPoint, 1f).SetEase(Ease.OutSine);
        }

        public void CompleteGroundZip(){
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
        }
    }
}