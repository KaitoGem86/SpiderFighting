using Animancer;

namespace Extensions.SystemGame.AIFSM{
    [System.Serializable]
    public class MyAnimationTransition<T> where T : ITransition {
        public T transition;
    }
}