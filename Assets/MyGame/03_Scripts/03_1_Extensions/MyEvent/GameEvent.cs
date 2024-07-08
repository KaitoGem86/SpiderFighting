using System.Collections.Generic;
using UnityEngine;

namespace MyTools.Event
{
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> _monoListeners = new List<GameEventListener>();
        private List<SerializeGameEventListener> _soListeners = new List<SerializeGameEventListener>();

        public void Raise()
        {
            for (int i = _monoListeners.Count - 1; i >= 0; i--)
            {
                _monoListeners[i].OnEventRaised();
            }

            for (int i = _soListeners.Count - 1; i >= 0; i--)
            {
                _soListeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!_monoListeners.Contains(listener))
            {
                _monoListeners.Add(listener);
            }
        }

        public void RegisterListener(SerializeGameEventListener listener)
        {
            if (!_soListeners.Contains(listener))
            {
                _soListeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (_monoListeners.Contains(listener))
            {
                _monoListeners.Remove(listener);
            }
        }

        public void UnregisterListener(SerializeGameEventListener listener)
        {
            if (_soListeners.Contains(listener))
            {
                _soListeners.Remove(listener);
            }
        }
    }

    public class GameEvent<T> : ScriptableObject where T : struct
    {
        private List<GameEventListener<T>> _monoListeners = new List<GameEventListener<T>>();
        private List<SerializeGameEventListener<T>> _soListeners = new List<SerializeGameEventListener<T>>();

        public void Raise(T value)
        {
            for (int i = _monoListeners.Count - 1; i >= 0; i--)
            {
                _monoListeners[i].OnEventRaised(value);
            }
        
            for (int i = _soListeners.Count - 1; i >= 0; i--)
            {
                _soListeners[i].OnEventRaised(value);
            }
        }

        public void RegisterListener(GameEventListener<T> listener)
        {
            if (!_monoListeners.Contains(listener))
            {
                _monoListeners.Add(listener);
            }
        }

        public void RegisterListener(SerializeGameEventListener<T> listener)
        {
            if (!_soListeners.Contains(listener))
            {
                _soListeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener<T> listener)
        {
            if (_monoListeners.Contains(listener))
            {
                _monoListeners.Remove(listener);
            }
        }

        public void UnregisterListener(SerializeGameEventListener<T> listener)
        {
            if (_soListeners.Contains(listener))
            {
                _soListeners.Remove(listener);
            }
        }
    }

    public class GameEvent<T0, T1> : ScriptableObject where T0 : struct where T1 : struct
    {
        private List<GameEventListener<T0, T1>> _listeners = new List<GameEventListener<T0, T1>>();
        private List<SerializeGameEventListener<T0, T1>> _soListeners = new List<SerializeGameEventListener<T0, T1>>();

        public void Raise(T0 value0, T1 value1)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value0, value1);
            }

            for (int i = _soListeners.Count - 1; i >= 0; i--)
            {
                _soListeners[i].OnEventRaised(value0, value1);
            }
        }

        public void RegisterListener(GameEventListener<T0, T1> listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RegisterListener(SerializeGameEventListener<T0, T1> listener)
        {
            if (!_soListeners.Contains(listener))
            {
                _soListeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener<T0, T1> listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void UnregisterListener(SerializeGameEventListener<T0, T1> listener)
        {
            if (_soListeners.Contains(listener))
            {
                _soListeners.Remove(listener);
            }
        }
    }
}