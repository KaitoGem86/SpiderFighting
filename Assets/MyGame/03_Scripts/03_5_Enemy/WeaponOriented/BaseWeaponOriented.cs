using Animancer;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class BaseWeaponOriented : MonoBehaviour
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] protected LayerMask _checkLayer;

        public void SetWeaponActive(bool value)
        {
            //_weapon.SetActive(value);
        }

        public virtual void OnWeaponAttack(Transform target, FSMState state) { }
        public WeaponType WeaponType => _weaponType;
    }
}