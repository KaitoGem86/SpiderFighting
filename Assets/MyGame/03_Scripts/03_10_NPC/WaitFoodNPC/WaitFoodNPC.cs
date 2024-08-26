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

        private WaitFoodNPCSO _so;

        public void Init(WaitFoodNPCSO so)
        {
            _so = so;
            _targetComponent.enabled = true;
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
                _targetComponent.enabled = false;
            }
        }
    }
}