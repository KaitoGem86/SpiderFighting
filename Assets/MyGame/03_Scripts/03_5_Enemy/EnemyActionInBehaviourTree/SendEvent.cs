using MyTools.Event;
using NodeCanvas.Framework;

namespace Core.GamePlay.Enemy{
    public class SendDefaultEvent : ActionTask{
        public DefaultEvent defaultEvent;

        protected override void OnExecute(){
            defaultEvent.Raise();
            EndAction(true);
        }
    }
}