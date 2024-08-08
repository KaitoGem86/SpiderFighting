using System.Collections;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    public class HealingBotController : BaseGadgetEquip
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private float _healingRate;
        [SerializeField] private float _height;
        private Transform _pivot;

        private void Awake(){
            _pivot = _playerController.blackBoard.HealingBotPivot;
        }

        private void OnEnable(){
            StartCoroutine(Healing());
        }

        private void OnDisable(){
            StopCoroutine(Healing());
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _pivot.position, Time.deltaTime * 5);
            var foward = new Vector3(_playerController.transform.position.x, _playerController.transform.position.y + _height, _playerController.transform.position.z) - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(foward), Time.deltaTime * 10);
        }
        
        public override void UseGadget()
        {
        }

        private IEnumerator Healing(){
            while (true){
                yield return new WaitForSeconds(_healingRate);
                //_playerController.Heal(1);
                Debug.Log("HealingBotController.Healing");
            }
        }
    }
}