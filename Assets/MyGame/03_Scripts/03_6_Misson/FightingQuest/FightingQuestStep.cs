using UnityEngine;

namespace Core.GamePlay.Mission{
    public class FightingQuestStep : QuestStep{
        
        public void Update(){
            if(Input.GetKeyDown(KeyCode.J))
                FinishStep();
        }
        
    }
}