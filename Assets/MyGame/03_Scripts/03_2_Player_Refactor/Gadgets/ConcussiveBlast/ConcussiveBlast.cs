using Core.GamePlay.Support;
using UnityEngine;

namespace Core.GamePlay.MyPlayer
{
    public class ConcussiveBlast : BaseGadgetEquip
    {
        [SerializeField] private BlastSO _blastSO;
        [SerializeField] private FindEnemyToAttack _findEnemyToAttack;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private bool _useLeftHand;

        private void Awake(){
            _blastSO.Init(1979);
        }

        public override void UseGadget()
        {
            var enemy = _findEnemyToAttack.FindEnemyByDistance(_playerController.transform, false);
            if(enemy != null){
                _blastSO.Spawn(_useLeftHand?_playerController.blackBoard.CurrentPlayerModel.leftHand : _playerController.blackBoard.CurrentPlayerModel.rightHand, enemy);
            }
            else{
                _blastSO.Spawn(_useLeftHand?_playerController.blackBoard.CurrentPlayerModel.leftHand : _playerController.blackBoard.CurrentPlayerModel.rightHand, _playerController.blackBoard.PlayerDisplay.forward);
            }
        }
    }
}