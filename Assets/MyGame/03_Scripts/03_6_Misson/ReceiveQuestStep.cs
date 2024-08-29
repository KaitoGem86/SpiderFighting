using Core.UI.Popup;
using UnityEngine;

namespace Core.GamePlay.Mission
{
    public class ReceiveQuestStep : QuestStep<ReceiveQuestInitData>
    {
        public bool isFade = false;

        public override void Init(Quest container)
        {
            base.Init(container);
            this.transform.position = _questData.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FadeScenePopup.Instance.Show(0.3f, 0.1f, FinishStep);
            }
        }
    }
}