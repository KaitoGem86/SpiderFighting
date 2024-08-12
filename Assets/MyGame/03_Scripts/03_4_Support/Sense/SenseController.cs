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
        }

        public void OnEnemyAttack()
        {
            _countEnemyAttack++;
            _countEnemyReady = Mathf.Max(0, --_countEnemyReady);
            warningEnemyReadyToAttack.SetActive(false);
            warningEnemyAttack.SetActive(true);
        }

        public void OnEnemyAttackEnd()
        {
            _countEnemyAttack = Mathf.Max(0, --_countEnemyAttack);
            warningEnemyAttack.SetActive(false);
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