using System.Collections.Generic;
using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    [CreateAssetMenu(menuName = "MyGame/Enemy/EnemyGroupSO")]
    public class EnemyGroupSO : ScriptableObject{
        [SerializeField] private int _maxEnemyCanAttack;
        [SerializeField] private DefaultSerializeEventListener _onEnemyAttack;
        [SerializeField] private DefaultSerializeEventListener _onEnemyAttackComplete;
        [HideInInspector] public List<EnemyController> enemyControllers;
        
        private int _currentEnemyAttack;

        public void AddEnemy(EnemyController enemyController){
            if (enemyControllers == null){
                _onEnemyAttack.RegisterListener();
                _onEnemyAttackComplete.RegisterListener();
                enemyControllers = new List<EnemyController>();
            }
            enemyControllers.Add(enemyController);
        }

        public void RemoveEnemy(EnemyController enemyController){
            enemyControllers.Remove(enemyController);
        }

        public void OnEnemyAttack(){
            _currentEnemyAttack++;
        }

        public void OnEnemyAttackComplete(){
            _currentEnemyAttack--;
        }

        public bool CheckAttack => _currentEnemyAttack < _maxEnemyCanAttack;
    }
}