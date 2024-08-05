using Animancer;

namespace Core.GamePlay.MyPlayer{
    public class FallingState : InAirState<ClipTransition>{
        public override void EnterState()
        {
            this.gameObject.SetActive(true);
        }
    }
}