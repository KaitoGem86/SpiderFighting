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
            if (currentTime <= 0)
            {
                FinishQuest(false);
            }
        }

        public override void FinishQuest(bool isWin = true)
        {
            onFinishShippingQuest.Raise();
            base.FinishQuest(isWin);
        }

        public override void NextQuestStep()
        {
            base.NextQuestStep();
            if (_currentStepIndex == 1)
            {
                _isStartQuest = true;
                onStartShippingQuest.Raise();
            }
        }
    }
}