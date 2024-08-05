using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class ShippingQuestStep : QuestStep<ShippingQuestInitData>
    {
        public override void Init(Quest container)
        {
            base.Init(container);
            this.transform.position = _questData.position;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FinishStep();
            }
        }
    }
}