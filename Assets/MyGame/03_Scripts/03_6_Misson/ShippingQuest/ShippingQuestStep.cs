using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class ShippingQuestStep : QuestStep<ShippingQuestData>
    {
        public override void Init(Quest container)
        {
            base.Init(container);
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