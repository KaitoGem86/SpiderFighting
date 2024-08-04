using UnityEngine;

namespace Core.GamePlay.Mission
{
    public abstract class QuestStep<T> : MonoBehaviour, IQuestStep where T : IMissionData
    {
        protected bool _isCompletedStep = false;
        protected Quest _container;
        protected T _questData;

        public virtual void Init(Quest container)
        {
            _container = container;
            //_questData = _container.GetData<T>();
        }

        public void FinishStep()
        {
            if (_isCompletedStep) return;
            _isCompletedStep = true;
            _container.NextQuestStep();
            Destroy(this.gameObject);
        }

        public GameObject GameObject => gameObject;
    }
}