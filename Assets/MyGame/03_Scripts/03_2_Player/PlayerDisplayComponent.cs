using System;
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
        [SerializeField]private AnimancerComponent _animacer;
        [SerializeField] private SerializedDictionary<PlayerTypeAnimMask, LayerControl> _dictLayerControls;
        

        public override void Init(PlayerController playerController)
        {
            base.Init(playerController);
            // for(int i = 0; i < _dictLayerControls.Count; i++){
            //     _dictLayerControls[(PlayerTypeAnimMask)i].Layer = _animacer.Layers[i];
            //     _dictLayerControls[(PlayerTypeAnimMask)i].Init();
            // }
        }

        public AnimancerState PlayAnimation(AnimationClip animationClip, float normalizedTime = 0f)
        {
            return _animacer.Play(animationClip);
        }

        public AnimancerState PlayAnimation(LinearMixerTransition manualMixerTransition, float normalizedTime = 0f, PlayerTypeAnimMask playerTypeAnimMask = PlayerTypeAnimMask.Base)
        {
            if(playerTypeAnimMask == PlayerTypeAnimMask.Base){
                return _animacer.Play(manualMixerTransition, normalizedTime);
            }
            return _dictLayerControls[playerTypeAnimMask].Layer.Play(manualMixerTransition, normalizedTime);
        }

        public AnimancerState PlayAnimation(AnimationClip animationClip, float normalizedTime, PlayerTypeAnimMask playerTypeAnimMask)
        {
            return _dictLayerControls[playerTypeAnimMask].Layer.Play(animationClip, normalizedTime);
        }

        public void StopAnimation(PlayerTypeAnimMask playerTypeAnimMask){
            _dictLayerControls[playerTypeAnimMask].Layer.StartFade(0, 0.5f);
        }

        public void ChangeBaseLayerToLayerAction(PlayerTypeAnimMask mask){
            var state = _dictLayerControls[PlayerTypeAnimMask.Base].Layer.CurrentState;
            _dictLayerControls[mask].Layer.Play(state.Clip);
            _dictLayerControls[PlayerTypeAnimMask.Base].Layer.StartFade(0, 0.5f);
        }

        public bool CheckHasAnimation(PlayerTypeAnimMask mask)
        {
            return _dictLayerControls[mask].Layer.IsPlayingAndNotEnding();
        }
    }
}