using Animancer;
using UnityEngine;
namespace Extensions.SystemGame.AIFSM
{
    public class BaseState<T> : MonoBehaviour where T : ITransition
    {
        [SerializeField] protected AIFSM _fsm;
        [SerializeField] protected AnimancerComponent _animancer;
        [SerializeField] protected T _transition;

        public AIState stateType;

        private void Awake(){
            this.gameObject.SetActive(false);
        }

        public virtual void EnterState()
        {
            this.gameObject.SetActive(true);
        }
        public virtual void Update() { }
        public virtual void ExitState()
        {
            this.gameObject.SetActive(false);
        }
    }
}