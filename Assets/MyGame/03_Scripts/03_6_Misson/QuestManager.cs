using System;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance;

        public Quest[] quests;
        private Quest currentQuest;
        private int currentQuestIndex;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            instance = this;
            StartQuest(0);
        }

        public void StartQuest(int questID)
        {
            currentQuestIndex = questID;
            currentQuest = quests[questID];
            quests[questID].Init();
            currentQuest.StartQuest();
        }

        public void NextQuest()
        {
            currentQuestIndex = (currentQuestIndex + 1) % quests.Length;
            currentQuest = quests[currentQuestIndex];
            currentQuest.Init();
            currentQuest.StartQuest();
        }

        public void FinishQuest()
        {
            currentQuest.FinishQuest();
        }
    }
}