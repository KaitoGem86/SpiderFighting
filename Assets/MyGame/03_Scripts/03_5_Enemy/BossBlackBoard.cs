using Core.GamePlay.Support;
using CustomEvent.DisplayInfo;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using UnityEngine;
namespace Core.GamePlay.Enemy
{
    public class BossBlackBoard : BaseEnemyBlackBoard
    {
        [Header("Boss")]
        public BossSO bossSO;
        public BossController bossController;
        public override IFSM fsm => bossController;
        public override IHitted hitted => bossController;
        public EnemyModel bossModel => currentModel;
        public FloatEvent onBossHPChange;
        public DisplayInfoEvent onBossActive;
    }
}