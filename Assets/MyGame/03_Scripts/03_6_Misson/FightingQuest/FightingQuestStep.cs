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

        public void Awake()
        {
            foreach (var enemyData in enemyDatas)
            {
                for (int i = 0; i < enemyData.count; i++)
                {
                    enemyData.enemySO.Spawn(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
                }
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
                FinishStep();
        }

    }
}