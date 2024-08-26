using Animancer;
using MyTools.ScreenSystem;

namespace Core.GamePlay.Mission.Protected
{
    public class NPCDeadState : NPCBaseState<ClipTransition>
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.controller.IsIgnore = true;
        }
    
        public void LoseGame()
        {
            _ScreenManager.Instance.ShowScreen<MissionResultPanel>(_ScreenTypeEnum.MissonResult)?.OnShow(false, default);
        }
    }
}