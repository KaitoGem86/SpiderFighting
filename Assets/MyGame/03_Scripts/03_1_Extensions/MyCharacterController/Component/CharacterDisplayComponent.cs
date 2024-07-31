using System;
using System.Collections.Generic;
using Animancer;
using AYellowpaper.SerializedCollections;
using Core.Test.Player;
using UnityEngine;
namespace Extensions.SystemGame.MyCharacterController
{
    public class CharacterDisplayComponent<T1> : BaseCharacterComponent<T1> where T1 : CharacterBlackBoard
    {
        [SerializeField] private List<CharacterModel> _characterModels;
        private AnimancerComponent _animacer;
        private CharacterModel _currentPlayerModel;
        private ClipTransition _currentClipTransition;
        private LinearMixerTransition _currentLinearMixerTransition;

        public override void Init(MyCharacterController<T1> controller)
        {
            base.Init(controller);
            _characterModels = _blackBoard.characterModels;
            _currentPlayerModel = null;
            _currentClipTransition = null;
            _currentLinearMixerTransition = null;
            ChangePlayerModel(0);
        }

        public void ChangePlayerModel(int modelIndex){
            _currentPlayerModel?.gameObject.SetActive(false);
            _currentPlayerModel = _characterModels[modelIndex];
            _currentPlayerModel.gameObject.SetActive(true);
            if(_currentClipTransition != null){
                var state = _currentPlayerModel.animancer.Play(_currentClipTransition);
                state.Events = _currentClipTransition.Events;
            }
            else if (_currentLinearMixerTransition != null){
                var state = _currentPlayerModel.animancer.Play(_currentLinearMixerTransition);
                state.Events = _currentLinearMixerTransition.Events;
            } 
            _blackBoard.CurrentModel = _currentPlayerModel;
        }

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