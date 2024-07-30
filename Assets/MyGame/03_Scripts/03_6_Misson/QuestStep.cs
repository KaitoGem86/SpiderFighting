using UnityEngine;

namespace Core.GamePlay.Mission
{
    public abstract class QuestStep : MonoBehaviour
    {
        protected bool _isCompletedStep = false;
        protected Quest _container;

        public virtual void Init(Quest container)
        {
            _container = container;
        }

        public void FinishStep()
        {
            if (_isCompletedStep) return;
            _isCompletedStep = true;
            _container.NextQuestStep();
            Destroy(this.gameObject);
        }
    }
}