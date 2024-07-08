using System;
using UnityEngine;
using UnityEngine.Events;

namespace MyTools.Event{
    // can phai override va danh dau System.Serializable cho cac lop event con
    public class SerializeGameEventListener{
        [SerializeField] private GameEvent _subject;
        [SerializeField] private UnityEvent _response;

        public void OnEventRaised(){
            _response.Invoke();
        }

        public void RegisterListener(){
            _subject.RegisterListener(this);
        }

        public void UnregisterListener(){
            _subject.UnregisterListener(this);
        }
    }

    public class SerializeGameEventListener<T> where T : struct{
        [SerializeField] private GameEvent<T> _subject;
        [SerializeField] private UnityEvent<T> _response;

        public void OnEventRaised(T value){
            _response?.Invoke(value);
        }

        public void RegisterListener(){
            _subject.RegisterListener(this);
        }

        public void UnregisterListener(){
            _subject.UnregisterListener(this);
        }
    }

    public class SerializeGameEventListener<T0, T1> where T0 : struct where T1 : struct{
        [SerializeField] private GameEvent<T0, T1> _subject;
        [SerializeField] private UnityEvent<T0, T1> _response;

        public void OnEventRaised(T0 value0, T1 value1){
            _response?.Invoke(value0, value1);
        }

        public void RegisterListener(){
            _subject.RegisterListener(this);
        }

        public void UnregisterListener(){
            _subject.UnregisterListener(this);
        }
    }
}