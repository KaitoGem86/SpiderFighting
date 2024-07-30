using System;
using System.Collections.Generic;
using Animancer;
using AYellowpaper.SerializedCollections;
using UnityEngine;
namespace Extentions.SystemGame.MyCharacterController
{
    public class CharacterDisplayComponent : BaseCharacterComponent
    {
        //[SerializeField] private List<PlayerModel> _playerModels;
        private AnimancerComponent _animacer;
        //private PlayerModel _currentPlayerModel;
        private ClipTransition _currentClipTransition;
        private LinearMixerTransition _currentLinearMixerTransition;
        private int _currentModelIndex = 0;

        public override void Init(MyCharacterController controller)
        {
            base.Init(controller);
            // _currentPlayerModel = _playerModels[0];
            // foreach (var item in _playerModels)
            // {
            //     item.gameObject.SetActive(false);
            // }
            // _currentPlayerModel.gameObject.SetActive(true);
            // _playerController.CurrentPlayerModel = _currentPlayerModel;
        }

        // public void ChangePlayerModel(){
        //     _currentPlayerModel.gameObject.SetActive(false);
        //     _currentPlayerModel = _playerModels[++_currentModelIndex % _playerModels.Count];
        //     _currentPlayerModel.gameObject.SetActive(true);
        //     if(_currentClipTransition != null){
        //         var state = _currentPlayerModel.animancer.Play(_currentClipTransition);
        //         state.Events = _currentClipTransition.Events;
        //     }
        //     else if (_currentLinearMixerTransition != null){
        //         var state = _currentPlayerModel.animancer.Play(_currentLinearMixerTransition);
        //         state.Events = _currentLinearMixerTransition.Events;
        //     } 
        //     _playerController.CurrentPlayerModel = _currentPlayerModel;
        
        // }

        public void ApplyRootMotion(bool value){
            _controller.useRootMotion = value;
        }

        public AnimancerState PlayAnimation(ClipTransition clipTransition){
            _currentClipTransition = clipTransition;
            _currentLinearMixerTransition = null;
            return _animacer.Play(clipTransition);
        }

        public AnimancerState PlayAnimation(LinearMixerTransition manualMixerTransition)
        {
            _currentClipTransition = null;
            _currentLinearMixerTransition = manualMixerTransition;
            return _animacer.Play(manualMixerTransition);
        }

        public AnimancerState PlayAnimation(LinearMixerTransition transition, float normalizeTime){
            var state = _animacer.Play(transition);
            return state;
        }
    }
}