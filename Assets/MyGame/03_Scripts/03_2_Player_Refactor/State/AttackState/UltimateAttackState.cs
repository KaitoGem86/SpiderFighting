using UnityEngine;
using NodeCanvas.StateMachines;

namespace Core.GamePlay.MyPlayer{
    public class UltimateAttackState : ClipTransitionPlayerState{
        [SerializeField] private GameObject _ultimateAttackEffect;
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
    }
}