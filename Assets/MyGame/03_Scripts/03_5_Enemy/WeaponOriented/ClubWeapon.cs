using Core.GamePlay.Support;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class ClubWeapon : BaseWeaponOriented
    {
        [SerializeField] float _range;
        [SerializeField] float _height;

        public override void OnWeaponAttack(Transform target, FSMState state)
        {
            var hit = Physics.OverlapCapsule(transform.position, transform.position + transform.up * _height, _range, _checkLayer);
            if (hit.Length > 0)
            {
                foreach (var item in hit)
                {
                    if (item.CompareTag("Player"))
                    {
                        item.GetComponent<IHitted>().HittedByPlayer(Extensions.SystemGame.AIFSM.FSMState.Hit);
                    }
                }
            }
        }
    }
}