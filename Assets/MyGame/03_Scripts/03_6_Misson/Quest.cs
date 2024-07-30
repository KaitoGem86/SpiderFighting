using UnityEngine;

namespace Core.GamePlay.Mission{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
    public class Quest : ScriptableObject{
        public QuestInfor infor;
        public RewardInfor reward;
        public QuestStep[] steps;

        private int _currentStepIndex = 0;

        public void StartQuest(){
            _currentStepIndex = 0;
            var go = Instantiate(steps[_currentStepIndex]);
            go.GetComponent<QuestStep>().Init(this);
        }

        public void NextQuestStep(){
            Debug.Log("Next Quest Step " + infor.QuestName);
            _currentStepIndex++;
            if (_currentStepIndex >= steps.Length){
                FinishQuest();
                return;
            }
            var go = Instantiate(steps[_currentStepIndex]);
            go.GetComponent<QuestStep>().Init(this);
        }

        public void FinishQuest(){
            Debug.Log("Finish Quest " + infor.QuestName);
        }
    }
}