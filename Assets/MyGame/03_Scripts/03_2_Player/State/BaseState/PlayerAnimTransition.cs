using System;
using Animancer;

namespace Core.GamePlay.Player{
    [Serializable]
    public class PlayerAnimTransition{
        public ClipTransition startAnimation;
        public ClipTransition keepAnimation;
        public ClipTransition endAnimation;
    }
}