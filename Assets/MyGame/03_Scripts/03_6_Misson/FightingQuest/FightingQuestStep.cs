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

    public class FightingQuestStep : QuestStep
    {
        [SerializeField] List<EnemyData> enemyDatas;
        private int _enemyCount;

        public void Awake()
        {
            _enemyCount = 0;
            foreach (var enemyData in enemyDatas)
            {
                for (int i = 0; i < enemyData.count; i++)
                {
                    var go = enemyData.enemySO.Spawn(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5))).GetComponent<EnemyController>();
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