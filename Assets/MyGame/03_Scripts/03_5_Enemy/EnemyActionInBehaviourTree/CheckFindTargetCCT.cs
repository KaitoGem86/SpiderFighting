using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using NodeCanvas.Framework;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class CheckFindTargetCCT : ConditionTask{
        public FindEnemyToAttack finder;
        public BBParameter<BaseEnemyBlackBoard> blackBoard;

        protected override bool OnCheck()
        {
            blackBoard.value.targetToAttack = finder.FindEnemyByDistance(blackBoard.value.transform, !blackBoard.value.hitted.IsPlayer);
            return blackBoard.value.targetToAttack != null;
        }
    }
}