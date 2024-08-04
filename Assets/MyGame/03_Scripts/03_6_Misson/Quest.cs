using UnityEngine;

namespace Core.GamePlay.Mission{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
    public class Quest : ScriptableObject{
        public QuestInfor infor;
        public RewardInfor reward;
        [SerializeField] private GameObject[] _stepPrefabs;
        
        private IQuestStep[] _steps;
        private IMissionData[] _data;

        private int _currentStepIndex = 0;

        public void Init(){
            _steps = new IQuestStep[_stepPrefabs.Length];
            _data = new IMissionData[_stepPrefabs.Length];
            for (int i = 0; i < _stepPrefabs.Length; i++){
                _steps[i] = _stepPrefabs[i].GetComponent<IQuestStep>();
            }
        }

        public void StartQuest(){
            _currentStepIndex = 0;
            var go = Instantiate(_steps[_currentStepIndex].GameObject);
            go.GetComponent<IQuestStep>().Init(this);
        }

        public void NextQuestStep(){
            Debug.Log("Next Quest Step " + infor.QuestName);
            _currentStepIndex++;
            if (_currentStepIndex >= _steps.Length){
                FinishQuest();
                return;
            }
            var go = Instantiate(_steps[_currentStepIndex].GameObject);
            go.GetComponent<IQuestStep>().Init(this);
        }

        public void FinishQuest(){
            Debug.Log("Finish Quest " + infor.QuestName);
        }

        public T GetData<T>() where T : IMissionData{
            if(_data[_currentStepIndex] is T t){
                return t;
            }
            throw new System.Exception("Data type is not match");
        }
    }
}