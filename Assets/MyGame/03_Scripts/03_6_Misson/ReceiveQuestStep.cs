using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class ReceiveQuestStep : QuestStep<ReceiveQuestInitData>
    {
        public override void Init(Quest container)
        {
            base.Init(container);
            this.transform.position = _questData.position;
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