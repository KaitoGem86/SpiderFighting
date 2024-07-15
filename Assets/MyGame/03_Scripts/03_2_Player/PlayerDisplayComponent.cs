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
        [SerializeField]private AnimancerComponent _animacer;
        [SerializeField] private SerializedDictionary<PlayerTypeAnimMask, LayerControl> _dictLayerControls;       

        public override void Init(PlayerController playerController)
        {
            base.Init(playerController);
        }

        public void ApplyRootMotion(bool value){
            _animacer.Animator.applyRootMotion = value;
        }

        public AnimancerState PlayAnimation(ClipTransition clipTransition){
            return _animacer.Play(clipTransition);
        }

        public AnimancerState PlayAnimation(LinearMixerTransition manualMixerTransition, float normalizedTime = 0f, PlayerTypeAnimMask playerTypeAnimMask = PlayerTypeAnimMask.Base)
        {
            if(playerTypeAnimMask == PlayerTypeAnimMask.Base){
                return _animacer.Play(manualMixerTransition);
            }
            return _dictLayerControls[playerTypeAnimMask].Layer.Play(manualMixerTransition, normalizedTime);
        }
    }
}