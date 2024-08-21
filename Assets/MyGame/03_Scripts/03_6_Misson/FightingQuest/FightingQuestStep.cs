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
        private Dictionary<EnemySO, List<GameObject>> enemies;


        public override void Init(Quest container)
        {
            base.Init(container);
            enemies ??= new Dictionary<EnemySO, List<GameObject>>();
            enemies.Clear();
            _enemyCount = 0;
            this.transform.position = _questData.position;
            foreach (var enemyData in _questData.data)
            {
                if (!enemies.ContainsKey(enemyData.enemySO))
                {
                    enemies.Add(enemyData.enemySO, new List<GameObject>());
                }
                for (int i = 0; i < enemyData.count; i++)
                {
                    Debug.Log("Spawn enemy " + _questData.position);
                    var go = enemyData.enemySO.Spawn(/*new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)) + _questData.position*/this.transform).GetComponent<BaseEnemyBlackBoard>();
                    go.onEnemyDead += UpdateRemainingEnemy;
                    enemies[enemyData.enemySO].Add(go.gameObject);
                }
                _enemyCount += enemyData.count;
            }
        }

        public void UpdateRemainingEnemy()
        {
            _enemyCount -= 1;
            if (_enemyCount <= 0)
            {
                FinishStep();
            }
        }

        public override void ResetStep()
        {
            DespawnEnemy();
            base.ResetStep();
        }

        public void DespawnEnemy()
        {
            foreach (var enemy in enemies)
            {
                foreach (var go in enemy.Value)
                {
                    if (go != null)
                    {
                        enemy.Key.DespawnObject(go);
                    }
                }
            }
        }
    }
}