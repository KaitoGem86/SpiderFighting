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
        [SerializeField] protected bool _canChangeToItself = false;

        private void Awake()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void EnterState()
        {
            this.gameObject.SetActive(true);
            var state = _animancer.Play(_transition);
            state.Time = 0;
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

        public bool CanChangeToItself
        {
            get { return _canChangeToItself; }
        }
    }
}