using MyTools.Event;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Shipping Quest")]
    public class ShippingQuest : Quest
    {
        public float time;
        public DefaultEvent onStartShippingQuest;
        public DefaultEvent onFinishShippingQuest;
        public FloatEvent onUpdateTime;
        public IntEvent onStepChange;
        private float currentTime;
        private bool _isStartQuest = false;

        public override void StartQuest()
        {
            base.StartQuest();
            currentTime = time;
            _isStartQuest = false;
        }

        public override void Update()
        {
            base.Update();
            if (!_isStartQuest) return;
            currentTime -= UnityEngine.Time.deltaTime;
            onUpdateTime.Raise(currentTime);
            if (currentTime <= 0)
            {
                FinishQuest(false, "Time out!" + "\n" + "You will lose all rewards!" + "\n" + "Do you want to retry?");
            }
        }

        public override void FinishQuest(bool isWin = true, string questText = null)
        {
            onFinishShippingQuest.Raise();
            base.FinishQuest(isWin, questText);
        }

        public override void NextQuestStep()
        {
            base.NextQuestStep();
            if (_currentStepIndex == 1)
            {
                _isStartQuest = true;
                onStartShippingQuest.Raise();
            }
            onStepChange.Raise(_currentStepIndex - 2);
        }
    }
}