using Animancer;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class BaseWeaponOriented : MonoBehaviour
    {
        [SerializeField] private WeaponType _weaponType;

        public void SetWeaponActive(bool value)
        {
            //_weapon.SetActive(value);
        }

        public virtual void OnWeaponAttack(Transform target) { }
        public WeaponType WeaponType => _weaponType;
    }
}