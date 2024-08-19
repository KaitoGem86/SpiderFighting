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
            var enemies = _fsm.blackBoard.FindEnemyToAttack.FindAllEnemyByDistance(_fsm.transform, 10);
            foreach (var enemy in _fsm.blackBoard.FindEnemyToAttack.FindAllEnemyByDistance(_fsm.transform, 10)){
                enemy.HittedByPlayer(Extensions.SystemGame.AIFSM.FSMState.KnockBack);
            }
        }
    }
}