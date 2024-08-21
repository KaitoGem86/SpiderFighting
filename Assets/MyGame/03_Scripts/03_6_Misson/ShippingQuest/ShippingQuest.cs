using UnityEngine;

namespace Core.GamePlay.Mission{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Shipping Quest")]
    public class ShippingQuest : Quest {
        public float time;
        private float currentTime;

        public override void StartQuest()
        {
            base.StartQuest();
            currentTime = time;
        }

        public override void Update()
        {
            base.Update();
            currentTime -= UnityEngine.Time.deltaTime;
            if(currentTime <= 0){
                FinishQuest(false);
            }
        }
    }
}