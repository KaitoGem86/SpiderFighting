using Animancer;

namespace Extensions.SystemGame.AIFSM
{
    public class ClipTransitionState<T> : BaseState<ClipTransition, T> where T : BlackBoard{}

    public class LinearMixerTransitionState<T> : BaseState<LinearMixerTransition, T> where T : BlackBoard{}
    public class ClipTransitionSequenceState<T> : BaseState<ClipTransitionSequence, T> where T : BlackBoard{}
}