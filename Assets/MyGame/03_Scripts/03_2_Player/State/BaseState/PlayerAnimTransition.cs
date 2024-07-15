using System;
using Animancer;

namespace Core.GamePlay.Player{
    [Serializable]
    public class PlayerAnimTransition{
        public ClipTransition startAnimation;
        public LinearMixerTransition keepAnimation;
        public ClipTransition endAnimation;
    }
}