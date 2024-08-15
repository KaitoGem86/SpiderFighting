using Core.GamePlay.Support;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class HandWeapon : BaseWeaponOriented
    {
        [SerializeField] LayerMask _checkLayer;
        [SerializeField] private float _range;
        public override void OnWeaponAttack(Transform target)
        {
            var hit = Physics.OverlapSphere(transform.position, _range, _checkLayer);
            if(hit.Length > 0)
            {
                foreach (var item in hit)
                {
                    if(item.CompareTag("Player"))
                    {
                        item.GetComponent<IHitted>().HittedByPlayer(Extensions.SystemGame.AIFSM.FSMState.Hit);
                    }
                }
            }
        }
    }
}