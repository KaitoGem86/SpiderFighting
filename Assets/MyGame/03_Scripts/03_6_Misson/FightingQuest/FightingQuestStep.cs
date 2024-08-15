using System.Collections.Generic;
using Core.GamePlay.Enemy;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    [System.Serializable]
    public class EnemyData
    {
        public EnemySO enemySO;
        public int count;
    }

    public class FightingQuestStep : QuestStep<FightingQuestInitData>
    {
        private int _enemyCount;

        public override void Init(Quest container)
        {
            base.Init(container);
            _enemyCount = 0;
            foreach (var enemyData in _questData.data)
            {
                for (int i = 0; i < enemyData.count; i++)
                {
                    var go = enemyData.enemySO.Spawn(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5))).GetComponent<BaseEnemyBlackBoard>();
                    go.onEnemyDead += UpdateRemainingEnemy;
                }
                _enemyCount += enemyData.count;
            }
        }

        public void UpdateRemainingEnemy(){
            _enemyCount -= 1;
            if(_enemyCount <= 0){
                FinishStep();
            }
        }
    }
}