using Animancer;
using Core.GamePlay.Player;
using UnityEngine;

namespace Core.GamePlay.Support{
    public class PreviewPlayerModels : MonoBehaviour{
        public AnimancerComponent animancer;
        public PlayerModel[] playerModels;
        private PlayerModel currentPlayerModel;
        public ClipTransitionSequence _previewClipTransition;

        public void PreviewModel(int index){
            if (index < 0 || index >= playerModels.Length) return;
            currentPlayerModel?.gameObject.SetActive(false);
            currentPlayerModel = playerModels[index];
            animancer.Animator = currentPlayerModel.animator;
            currentPlayerModel.gameObject.SetActive(true);
            if(_previewClipTransition != null){
                var state = animancer.Play(_previewClipTransition);
                state.Time = 0;
            }
        }
    }
}