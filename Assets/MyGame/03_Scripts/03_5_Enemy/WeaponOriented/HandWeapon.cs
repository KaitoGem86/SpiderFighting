using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class HandWeapon : BaseWeaponOriented
    {
        public override void OnWeaponAttack(Transform target)
        {
            var go = Physics.OverlapSphere(transform.position, 0.5f);
            foreach (var item in go)
            {
                if (item.CompareTag("Player"))
                {
                    Debug.Log("HandWeapon Attack " + item.name);
                }
            }
        }
    }
}