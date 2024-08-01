using UnityEngine;
namespace Extensions.SystemGame.AIFSM
{
    public class BaseState : ScriptableObject
    {
        public virtual void EnterState(AIFSM fsm)
        {
        }
        public virtual void UpdateState(AIFSM fsm) { }
        public virtual void ExitState(AIFSM fsm) { }
    }
}