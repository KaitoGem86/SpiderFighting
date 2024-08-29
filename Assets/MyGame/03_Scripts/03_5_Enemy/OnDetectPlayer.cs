using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class OnDetectPlayer : MonoBehaviour
    {
        [SerializeField] private BossBlackBoard _blackBoard;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _blackBoard.onBossActive.Raise(_blackBoard.bossSO.info);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _blackBoard.onBossHPChange.Raise(-1);
            }
        }
    }
}