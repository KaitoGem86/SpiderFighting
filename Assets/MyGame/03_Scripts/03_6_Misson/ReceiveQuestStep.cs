using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class ReceiveQuestStep : QuestStep
    {
        public override void Init(Quest container)
        {
            base.Init(container);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FinishStep();
            }
        }
    }
}