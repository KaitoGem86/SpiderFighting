using UnityEngine;
using NodeCanvas.StateMachines;

namespace Core.GamePlay.MyPlayer{
    public class UltimateAttackState : ClipTransitionPlayerState{
        [SerializeField] private GameObject _ultimateAttackEffect;
        public void CompleteAttack(){
            _fsm.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.FallingDown);
        }
    }
}