using System;
using System.Collections.Generic;
using Animancer;
using AYellowpaper.SerializedCollections;
using UnityEngine;
namespace Core.GamePlay.Player
{
    public enum PlayerTypeAnimMask{
        Base,
        Upper,
        Lower
    }

    [Serializable]
    public class LayerControl{
        public AvatarMask AvatarMask;
        public AnimancerLayer Layer;
        public void Init(){
            Layer.SetMask(AvatarMask);
            Layer.Weight = 1;
        }
    }

    public class PlayerDisplayComponent : BasePlayerComponent
    {
        [SerializeField] private List<PlayerModel> _playerModels;
        private AnimancerComponent _animacer => _currentPlayerModel.animancer;
        private PlayerModel _currentPlayerModel;
        private ClipTransition _currentClipTransition;
        private LinearMixerTransition _currentLinearMixerTransition;
        private int _currentModelIndex = 0;

        public override void Init(PlayerController playerController)
        {
            base.Init(playerController);
            _currentPlayerModel = _playerModels[0];
            foreach (var item in _playerModels)
            {
                item.gameObject.SetActive(false);
            }
            _currentPlayerModel.gameObject.SetActive(true);
            _playerController.CurrentPlayerModel = _currentPlayerModel;
        }

        public void ChangePlayerModel(){
            _currentPlayerModel.gameObject.SetActive(false);
            var currentState = _currentPlayerModel.animancer;
            _currentPlayerModel = _playerModels[++_currentModelIndex % _playerModels.Count];
            _currentPlayerModel.gameObject.SetActive(true);
            if(_currentClipTransition != null){
                var state = _currentPlayerModel.animancer.Play(_currentClipTransition);
                state.Events = _currentClipTransition.Events;
            }
            else if (_currentLinearMixerTransition != null){
                var state = _currentPlayerModel.animancer.Play(_currentLinearMixerTransition);
                state.Events = _currentLinearMixerTransition.Events;
            } 
            _playerController.CurrentPlayerModel = _currentPlayerModel;
        
        }

        public void ApplyRootMotion(bool value){
            _animacer.Animator.applyRootMotion = value;
        }

        public AnimancerState PlayAnimation(ClipTransition clipTransition){
            _currentClipTransition = clipTransition;
            _currentLinearMixerTransition = null;
            return _animacer.Play(clipTransition);
        }

        public AnimancerState PlayAnimation(LinearMixerTransition manualMixerTransition, float normalizedTime = 0f, PlayerTypeAnimMask playerTypeAnimMask = PlayerTypeAnimMask.Base)
        {
            _currentClipTransition = null;
            _currentLinearMixerTransition = manualMixerTransition;
            return _animacer.Play(manualMixerTransition);
            //return _dictLayerControls[playerTypeAnimMask].Layer.Play(manualMixerTransition, normalizedTime);
        }
    }
}