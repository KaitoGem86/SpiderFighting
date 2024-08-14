using Animancer;
using NodeCanvas.Framework;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class ExitStateByAnim : ActionTask
    {
        public AnimationClip anim;
        public BBParameter<AnimancerComponent> animancer;
        public BBParameter<EnemyBlackBoard> blackBoard;
        public string des;

        protected override string info => des;

        protected override void OnExecute()
        {
            var state = animancer.value.Play(anim);
            state.Events.OnEnd = () =>
            {
                blackBoard.value.controller.ChangeAction(Extensions.SystemGame.AIFSM.FSMState.Idle);
                EndAction(true);
            };
        }
    }
}