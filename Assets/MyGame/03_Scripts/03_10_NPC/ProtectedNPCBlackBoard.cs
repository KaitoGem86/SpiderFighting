using Animancer;
using Extensions.SystemGame.AIFSM;

namespace Core.GamePlay.Mission.Protected{
    public class ProtectedNPCBlackBoard : BlackBoard{
        public AnimancerComponent animancer;
        public bool isPlayer;
        public float hp;
        public NeedProtectedNPC controller;
    }
}