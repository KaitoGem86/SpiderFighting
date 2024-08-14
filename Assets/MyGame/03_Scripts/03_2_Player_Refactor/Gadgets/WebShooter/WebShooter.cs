using Core.GamePlay.Support;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class WebShooter : BaseGadgetEquip
    {
        [SerializeField] private WebBulletSO _webBulletSO;
        [SerializeField] private FindEnemyToAttack _findEnemyToAttack;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private bool _useLeftHand;

        private /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _webBulletSO.Init(1978);
        }

        public override void UseGadget()
        {
            var enemy = _findEnemyToAttack.FindEnemyByDistance(_playerController.transform, false);
            if (enemy != null)
            {
                _webBulletSO.Spawn(_useLeftHand?_playerController.blackBoard.CurrentPlayerModel.leftHand : _playerController.blackBoard.CurrentPlayerModel.rightHand, enemy);
                _useLeftHand = !_useLeftHand;
            }
            else{
                _webBulletSO.Spawn(_useLeftHand?_playerController.blackBoard.CurrentPlayerModel.leftHand : _playerController.blackBoard.CurrentPlayerModel.rightHand, _playerController.blackBoard.PlayerDisplay.forward);
                _useLeftHand = !_useLeftHand;
            }
        }
    }
}