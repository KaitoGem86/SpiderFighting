using UnityEngine;

namespace Core.GamePlay.Player{
    [CreateAssetMenu(fileName = nameof(ZipAction), menuName = ("GamePlay/Player/State/MovementState/" + nameof(ZipAction)), order = 0)]
    public class ZipAction : LocalmotionAction{
        private GameObject _displayZipPoint;

        public override void Init(PlayerController playerController, ActionEnum actionEnum)
        {
            base.Init(playerController, actionEnum);
            _displayZipPoint = playerController.DisplayZipPoint;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

        }

        private void EndZip(){
            
        }
    }
}