using UnityEngine;  
using Animancer;
using System.Collections;

namespace Collectible{
    public class ChestController : CollectibleController{
        [SerializeField] private AnimancerComponent _animancerComponent;
        [SerializeField] private ClipTransitionSequence _openClip;
        [SerializeField] private ClipTransition _defaultClip;

        public override void Init(CollectibleData data, CollectibleSO so, int id)
        {
            base.Init(data, so, id);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _animancerComponent.Play(_defaultClip);
        }

        public override void OnCollect()
        {
            _animancerComponent.Play(_openClip);
            StartCoroutine(Collect());   
        }

        private IEnumerator Collect()
        {
            yield return new WaitForSeconds(3f);
            base.OnCollect();
        }
    }
}