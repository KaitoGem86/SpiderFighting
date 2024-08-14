using System.Collections.Generic;
using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    [CreateAssetMenu(menuName = "MyGame/Enemy/EnemyGroupSO")]
    public class EnemyGroupSO : ScriptableObject
    {
        [SerializeField] private int _maxEnemyCanAttack;
        [SerializeField] private DefaultSerializeEventListener _onEnemyAttack;
        [SerializeField] private DefaultSerializeEventListener _onEnemyAttackComplete;
        [HideInInspector] public List<EnemyController> enemyControllers;

        private int _currentEnemyAttack;

        public void Dispose()
        {
            Debug.Log("OnApplicationQuit");
            _onEnemyAttack.UnregisterListener();
            _onEnemyAttackComplete.UnregisterListener();
            enemyControllers.Clear();
            enemyControllers = null;
        }

        public void Init()
        {
            Debug.Log("Enable");
            _currentEnemyAttack = 0;
            _onEnemyAttack.RegisterListener();
            _onEnemyAttackComplete.RegisterListener();
            enemyControllers = new List<EnemyController>();
        }

        public void AddEnemy(EnemyController enemyController)
        {
            enemyControllers.Add(enemyController);
        }

        public void RemoveEnemy(EnemyController enemyController)
        {
            enemyControllers.Remove(enemyController);
        }

        public void OnEnemyAttack()
        {
            _currentEnemyAttack++;
            Debug.Log("OnEnemyAttack " + _currentEnemyAttack);
        }

        public void OnEnemyAttackComplete()
        {
            _currentEnemyAttack--;
            _currentEnemyAttack = Mathf.Max(0, _currentEnemyAttack);
            Debug.Log("OnEnemyAttackComplete " + _currentEnemyAttack);
        }

        public bool CheckAttack {
            get {
                Debug.Log("CheckAttack " + _currentEnemyAttack + " " + _maxEnemyCanAttack);
                return _currentEnemyAttack < _maxEnemyCanAttack;
            } 
        } 
    }
}