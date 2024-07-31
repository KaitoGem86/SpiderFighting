using System.Collections.Generic;
using Extensions.SystemGame.MyCharacterController;
using UnityEngine;

namespace Core.Test.Player{
    [CreateAssetMenu(menuName = "MyGame/Player/Components/PlayerDisplayComponent")]
    public class PlayerDisplayComponent : CharacterDisplayComponent<PlayerController, PlayerBlackBoard>
    {
        private List <PlayerModels> _playerModels;
        private PlayerModels _currentPlayerModel;
        private int _currentModelIndex;

        public override void Init(PlayerBlackBoard blackBoard)
        {
            base.Init(blackBoard);
            _playerModels = blackBoard.models;
        }

        public void ChangePlayerModel(){
            _currentPlayerModel.gameObject.SetActive(false);
            _currentPlayerModel = _playerModels[++_currentModelIndex % _playerModels.Count];
            _currentPlayerModel.gameObject.SetActive(true);
            // if(_currentClipTransition != null){
            //     var state = _currentPlayerModel.animancer.Play(_currentClipTransition);
            //     state.Events = _currentClipTransition.Events;
            // }
            // else if (_currentLinearMixerTransition != null){
            //     var state = _currentPlayerModel.animancer.Play(_currentLinearMixerTransition);
            //     state.Events = _currentLinearMixerTransition.Events;
            // } 
            //.CurrentPlayerModel = _currentPlayerModel;
        }

    }
}