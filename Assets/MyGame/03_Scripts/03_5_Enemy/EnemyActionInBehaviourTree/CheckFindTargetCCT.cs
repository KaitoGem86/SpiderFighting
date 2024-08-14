using Core.GamePlay.Support;
using NodeCanvas.Framework;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class CheckFindTargetCCT : ConditionTask{
        public FindEnemyToAttack finder;
        public BBParameter<EnemyBlackBoard> blackBoard;

        private EnemyController finderController;

        protected override bool OnCheck()
        {
            finderController = blackBoard.value.controller;
            blackBoard.value.targetToAttack = finder.FindEnemyByDistance(finderController.transform, !finderController.IsPlayer);
            return blackBoard.value.targetToAttack != null;
        }
    }
}