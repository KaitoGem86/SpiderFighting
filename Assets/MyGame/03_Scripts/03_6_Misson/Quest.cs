using System.Collections.Generic;
using Core.GamePlay.Mission.Protected;
using MyTools.Event;
using MyTools.ScreenSystem;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest/New Quest")]
    public class Quest : ScriptableObject
    {
        public QuestInfor infor;
        public RewardInfor reward;
        [SerializeField] private List<GameObject> _stepPrefabs;
        [SerializeField] private List<ScriptableObject> _dataPrefabs;

        private IQuestStep[] _steps;
        private IMissionData[] _data;

        private IQuestStep _currentQuestStep;

        private int _currentStepIndex = 0;

        public void Init()
        {
            _steps = new IQuestStep[_stepPrefabs.Count];
            _data = new IMissionData[_stepPrefabs.Count];
            for (int i = 0; i < _stepPrefabs.Count; i++)
            {
                _steps[i] = _stepPrefabs[i].GetComponent<IQuestStep>();
            }
            for (int i = 0; i < _dataPrefabs.Count; i++)
            {
                _data[i] = _dataPrefabs[i] as IMissionData;
            }
        }

        public virtual void StartQuest()
        {
            _currentStepIndex = 0;
            var go = Instantiate(_steps[_currentStepIndex].GameObject);
            go.GetComponent<IQuestStep>().Init(this);
            _currentQuestStep = go.GetComponent<IQuestStep>();
        }

        public virtual void NextQuestStep()
        {
            _currentStepIndex++;
            if (_currentStepIndex >= _steps.Length)
            {
                FinishQuest();
                return;
            }
            var go = Instantiate(_steps[_currentStepIndex].GameObject);
            go.GetComponent<IQuestStep>().Init(this);
            _currentQuestStep = go.GetComponent<IQuestStep>();
        }

        public virtual void FinishQuest(bool isWin = true)
        {
            QuestManager.instance.FinishQuest();
            _ScreenManager.Instance.ShowScreen<MissionResultPanel>(_ScreenTypeEnum.MissonResult)?.OnShow(isWin, reward);
        }

        public virtual void ResetQuest(){
            _currentQuestStep.ResetStep();
        }

        public virtual void Update(){

        }

        public T GetData<T>() where T : IMissionData
        {
            if (_data[_currentStepIndex] is T t)
            {
                return t;
            }
            throw new System.Exception("Data type is not match");
        }

        public List<GameObject> stepPrefabs
        {
            get => _stepPrefabs;
            set => _stepPrefabs = value;
        }

        public List<ScriptableObject> dataPrefabs
        {
            get => _dataPrefabs;
            set => _dataPrefabs = value;
        }
    }
}