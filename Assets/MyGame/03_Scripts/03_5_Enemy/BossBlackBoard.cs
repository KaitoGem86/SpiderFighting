using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using MyTools.Event;
using UnityEngine;
namespace Core.GamePlay.Enemy
{
    public class BossBlackBoard : BaseEnemyBlackBoard
    {
        [Header("Boss")]
        public BossController bossController;
        public override IFSM fsm => bossController;
        public override IHitted hitted => bossController;
        public EnemyModel bossModel => currentModel;
        public FloatEvent onBossHPChange;
    }
}