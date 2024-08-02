using Animancer;
using UnityEngine;

public class TestSequence : MonoBehaviour{
    [SerializeField] private AnimancerComponent _animancer;
    [SerializeField] private ClipTransitionSequence _sequence;

    public void Start(){
        var state = _animancer.Play(_sequence);
        state.Time = 0;
    }
}