using UnityEngine;
using NodeCanvas.StateMachines;
using System.Collections;
using Cinemachine;

namespace Core.GamePlay.MyPlayer{
    public class UltimateAttackState : ClipTransitionPlayerState{
        [SerializeField] private GameObject _ultimateAttackEffect;

        public override void EnterState()
        {
            base.EnterState();
            //_blackBoard.CameraUltimate.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;
           //_blackBoard.CameraDefault.Priority = _blackBoard.defaultPriority;
            //_blackBoard.CameraUltimate.Priority = _blackBoard.topPriority;
        }

        public override void ExitState()
        {
            //_blackBoard.CameraDefault.Priority = _blackBoard.topPriority;
            //_blackBoard.CameraUltimate.Priority = _blackBoard.defaultPriority;
            base.ExitState();
        }

        public override void Update()
        {
            base.Update();
            _blackBoard.CameraUltimate.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += Time.deltaTime * 5;
        }

        public void CompleteAttack(){
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.FallingDown);
        }

        public void ApplyDamage(){
            Physics.queriesHitTriggers = true;
            var enemies = _fsm.blackBoard.FindEnemyToAttack.FindAllEnemyByDistance(_fsm.transform, 10, !_blackBoard.PlayerController.IsPlayer);
            foreach (var enemy in enemies){
                enemy.HittedByPlayer(Extensions.SystemGame.AIFSM.FSMState.KnockBack);
                _blackBoard.AttackCount += 1;
            }
            _blackBoard.ResetTime();
            _blackBoard.OnShowHitCounter.Raise(_blackBoard.AttackCount);
        }

        // private IEnumerator ControllUltimateCamera(){
        //     int pathPosition = 0;
        //     int maxPathPosition = _blackBoard.path.m_Waypoints.Length;
        //     while (pathPosition < maxPathPosition){
        //         _blackBoard.CameraUltimate.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = pathPosition;
        //         pathPosition++;
        //         yield return new WaitForSeconds(0.3f);
        //     }
        // }
    }
}