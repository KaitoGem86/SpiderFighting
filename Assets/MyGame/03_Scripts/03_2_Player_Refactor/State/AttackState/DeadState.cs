using Animancer;
using Core.GamePlay.Mission.Protected;

namespace Core.GamePlay.MyPlayer{
    public class DeadState : BasePlayerState<ClipTransitionSequence>{
        public override void EnterState()
        {
            _blackBoard.Character.useRootMotion = true;
            _blackBoard.PlayerController.IsIgnore = true;
            base.EnterState();
        }

        public void CompleteDead()
        {
            if (_blackBoard.PlayerData.isInMission)
            {
                MissionResultPanel.Instance.Show(false, default, "You will lose all reward!!!" + "\n" + "Do you want to retry?");
            }
            else
            {
                MissionResultPanel.Instance.Show(false, default, "Continue Fighting?");
            }
        }

        public override void ExitState()
        {
            _blackBoard.PlayerController.IsIgnore = false;
            _blackBoard.Character.useRootMotion = false;
            base.ExitState();
        }
    }
}