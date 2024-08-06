using Animancer;
using UnityEngine;
namespace Extensions.SystemGame.AIFSM
{
    public class BaseState<T1, T2> : MonoBehaviour, IState where T1 : ITransition where T2 : BlackBoard
    {
        [SerializeField] protected FSM<T2> _fsm;
        [SerializeField] protected AnimancerComponent _animancer;
        protected T1 _transition;
        [SerializeField] protected T1[] _transitions;
        [SerializeField] protected FSMState _stateType;
        [SerializeField] protected bool _canChangeToItself = false;

        protected int _currentTransitionIndex;

        protected virtual void Awake()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void EnterState()
        {
            this.gameObject.SetActive(true);
            _currentTransitionIndex = GetIndexTransition();
            _transition = _transitions[_currentTransitionIndex];
            var state = _animancer.Play(_transition );
            state.Time = 0;
        }
        public virtual void Update() { }
        public virtual void ExitState()
        {
            this.gameObject.SetActive(false);
        }

        public FSMState StateType
        {
            get { return _stateType; }
        }

        public bool CanChangeToItself
        {
            get { return _canChangeToItself; }
        }

        protected virtual int GetIndexTransition(){
            return Random.Range(0, _transitions.Length);
        }
    }
}