using Animancer;
using UnityEngine;
namespace Extensions.SystemGame.AIFSM
{
    public class BaseState<T> : MonoBehaviour, IState where T : ITransition
    {
        [SerializeField] protected AIFSM _fsm;
        [SerializeField] protected AnimancerComponent _animancer;
        [SerializeField] protected T _transition;
        [SerializeField] protected AIState _stateType;

        private void Awake()
        {
            this.gameObject.SetActive(false);
            if (this is IState)
                Debug.Log("IState " + gameObject.name);
        }

        public virtual void EnterState()
        {
            this.gameObject.SetActive(true);
            _animancer.Play(_transition);
        }
        public virtual void Update() { }
        public virtual void ExitState()
        {
            this.gameObject.SetActive(false);
        }

        public AIState StateType
        {
            get { return _stateType; }
        }
    }
}