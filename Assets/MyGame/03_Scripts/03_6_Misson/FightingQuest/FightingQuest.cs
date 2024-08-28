using Core.Manager;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Fighting Quest")]
    public class FightingQuest : Quest
    {
        public float range;
        private bool _isStartQuest = false;
        private bool _isCompleted = false;

        public override void StartQuest()
        {
            base.StartQuest();
            _isCompleted = false;
            _isStartQuest = false;
        }

        public override void NextQuestStep()
        {
            base.NextQuestStep();
            if (_currentStepIndex == 1)
            {
                _isStartQuest = true;
            }
        }

        public override void FinishQuest(bool isWin = true, string questText = null)
        {
            base.FinishQuest(isWin, questText);
            _isCompleted = true;
            _isStartQuest = false;
        }

        public override void Update()
        {
            if (!_isStartQuest) return;
            if (_isCompleted) return;
            if (Vector3.Distance(_currentQuestStep.GameObject.transform.position, GameManager.Instance.playerBlackBoard.transform.position) > range)
            {
                FinishQuest(false, "Out of range!" + "\n" + "You will lose all rewards!" + "\n" + "Do you want to retry?");
            }
        }
    }
}