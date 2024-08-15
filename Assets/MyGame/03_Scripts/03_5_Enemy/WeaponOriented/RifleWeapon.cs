using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class RifleWeapon : BaseWeaponOriented
    {
        
        public override void OnWeaponAttack(Transform target)
        {
            Debug.Log("PistolWeapon Attack");
        }
    }
}