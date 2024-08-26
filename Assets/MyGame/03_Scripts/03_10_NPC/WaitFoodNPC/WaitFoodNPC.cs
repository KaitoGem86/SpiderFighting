using Animancer;
using UnityEngine;

namespace Core.GamePlay.Mission.NPC
{
    public class WaitFoodNPC : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent _animancerComponent;
        [SerializeField] private ClipTransition _waitClip;
        [SerializeField] private ClipTransition[] _receiveClip;
        [SerializeField] private Animator[] _animators;
        [SerializeField] private Target _targetComponent;
        [SerializeField] private Collider _collider;

        private WaitFoodNPCSO _so;
        private ShippingQuestStep _stepContainer;

        public void Init(WaitFoodNPCSO so, ShippingQuestStep stepContainer)
        {
            _so = so;
            _targetComponent.enabled = true;
            _stepContainer = stepContainer;
            int index = Random.Range(0, _animators.Length);
            foreach (var animator in _animators)
            {
                animator.gameObject.SetActive(false);
            }
            _animators[index].gameObject.SetActive(true);
            _animancerComponent.Animator.avatar = _animators[index].avatar;
            _animancerComponent.Play(_waitClip);
        }

        public void ReceiveFood()
        {
            _animancerComponent.Play(_receiveClip[Random.Range(0, _receiveClip.Length)]);
        }

        public void OnTriggerEnter(Collider other){
            if (other.CompareTag("Player"))
            {
                //_so.DespawnObject(gameObject);
                ReceiveFood();
                _stepContainer.OnCompleteAnShipping();
                _targetComponent.enabled = false;
                _collider.enabled = false;
            }
        }
    }
}