using Animancer;
using UnityEngine;

namespace Extensions.SystemGame.AIFSM
{
    public class BlackBoard : MonoBehaviour
    {
        [Header("Animation")]
        public AnimancerComponent animancer;
        public AnimancerTransitionAsset idle;
    }
}