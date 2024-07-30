using System;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class QuestManager : MonoBehaviour
    {
        public Quest[] quests;
        private Quest currentQuest;
        public static Action OnQuestStepCompleted;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            StartQuest(0);
        }

        public void StartQuest(int questID)
        {
            currentQuest = quests[questID];
            currentQuest.StartQuest();
        }

        public void NextQuestStep()
        {
            currentQuest.NextQuestStep();
        }

        public void FinishQuest()
        {
            currentQuest.FinishQuest();
        }
    }
}