using UnityEngine;

namespace Core.GamePlay.Support
{
    public class SenseController : MonoBehaviour
    {
        public GameObject warningEnemyReadyToAttack;
        public GameObject warningEnemyAttack;
        private int _countEnemyReady = 0;
        private int _countEnemyAttack = 0;

        public void OnEnemyReadyToAttack()
        {
            _countEnemyReady++;
            warningEnemyReadyToAttack.SetActive(true);
            warningEnemyAttack.SetActive(false);
            Debug.Log("OnEnemyReadyToAttack " + _countEnemyAttack + " " + _countEnemyReady);
        }

        public void OnEnemyAttack()
        {
            _countEnemyAttack++;
            _countEnemyReady = Mathf.Max(0, --_countEnemyReady);
            warningEnemyReadyToAttack.SetActive(false);
            warningEnemyAttack.SetActive(true);
            Debug.Log("OnEnemyAttack " + _countEnemyAttack + " " + _countEnemyReady);
        }

        public void OnEnemyAttackEnd()
        {
            _countEnemyAttack = Mathf.Max(0, --_countEnemyAttack);
            warningEnemyAttack.SetActive(false);
            Debug.Log("OnEnemyAttackEnd " + _countEnemyAttack + " " + _countEnemyReady);
            if (_countEnemyAttack > 0)
            {
                warningEnemyAttack.SetActive(true);
                return;
            }
            if (_countEnemyReady > 0)
            {
                warningEnemyReadyToAttack.SetActive(false);
                return;
            }
        }
    }
}