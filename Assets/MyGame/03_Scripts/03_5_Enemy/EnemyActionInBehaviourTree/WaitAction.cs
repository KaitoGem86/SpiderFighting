using NodeCanvas.Framework;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class WaitAction : ActionTask{
        protected override void OnExecute(){
            Debug.Log("WaitAction OnExecute");
            EndAction(true);
        }
    }
}