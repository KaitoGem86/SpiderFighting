using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class HandWeapon : BaseWeaponOriented
    {
        [SerializeField] private float _range;
        [SerializeField] private ParticleSystem _particle;
        public override void OnWeaponAttack(Transform target, FSMState state)
        {
            var hit = Physics.OverlapSphere(transform.position, _range, _checkLayer);
            if(hit.Length > 0)
            {
                foreach (var item in hit)
                {
                    Debug.Log(item.name);
                    if(item.CompareTag("Player"))
                    {
                        item.GetComponent<IHitted>().HittedByPlayer(state);
                        _particle.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}