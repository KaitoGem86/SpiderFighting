using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class RifleWeapon : BaseWeaponOriented
    {
        
        public override void OnWeaponAttack(Transform target, FSMState state)
        {
            Debug.Log("PistolWeapon Attack");
        }
    }
}