using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Event{
    //[CreateAssetMenu(fileName = nameof(GameEventListener), menuName = ("MyTools/" + nameof(GameEventListener)), order = 0)]
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _subject;
        [SerializeField] private UnityEvent _response;

        protected virtual void OnEnable()
        {
            _subject.RegisterListener(this);
        }

        protected virtual void OnDisable()
        {
            _subject.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            _response.Invoke();
        }
    }    

    //[CreateAssetMenu(fileName = nameof(GameEventListener), menuName = ("MyTools/" + nameof(GameEventListener)), order = 0)]
    public class GameEventListener<T> : MonoBehaviour where T : struct
    {
        [SerializeField] private GameEvent<T> _subject;
        [SerializeField] private bool _unregisterOnDisable = true;
        [SerializeField] private UnityEvent<T> _response;
    
        protected virtual void OnEnable()
        {
            _subject.RegisterListener(this);
        }
    
        protected virtual void OnDisable()
        {
            if (_unregisterOnDisable)
                _subject.UnregisterListener(this);
        }

        protected virtual void OnDestroy()
        {
            if (!_unregisterOnDisable)
                _subject.UnregisterListener(this);
        }
    
        public void OnEventRaised(T value)
        {
            _response?.Invoke(value);
        }
    }

    public class GameEventListener<T0, T1> : MonoBehaviour where T0 : struct where T1 : struct
    {
        [SerializeField] private GameEvent<T0, T1> _subject;
        [SerializeField] private UnityEvent<T0, T1> _response;
    
        protected virtual void OnEnable()
        {
            _subject.RegisterListener(this);
        }
    
        protected virtual void OnDisable()
        {
            _subject.UnregisterListener(this);
        }
    
        public void OnEventRaised(T0 value0, T1 value1)
        {
            _response?.Invoke(value0, value1);
        }
    }
}