using System.Collections.Generic;
using Extensions.SystemGame.AIFSM;
using UnityEngine;

namespace Core.GamePlay.Enemy{
    public class EnemyWeaponController : MonoBehaviour{
        [SerializeField] private List<BaseWeaponOriented> _weapons;

        private Dictionary<WeaponType, BaseWeaponOriented> _weaponDict = new Dictionary<WeaponType, BaseWeaponOriented>();
        private WeaponType _currentWeaponType;

        private void Awake(){
            foreach (var weapon in _weapons){
                _weaponDict.Add(weapon.WeaponType, weapon);
            }
        }

        public void SetTypeOfEnemy(WeaponType type, Transform parent){
            _weaponDict[_currentWeaponType].gameObject.SetActive(false);
            _weaponDict[_currentWeaponType].transform.SetParent(this.transform);
            _currentWeaponType = type;
            _weaponDict[_currentWeaponType].gameObject.SetActive(true);
            _weaponDict[_currentWeaponType].transform.SetParent(parent);
            _weaponDict[_currentWeaponType].transform.localPosition = Vector3.zero;
            _weaponDict[_currentWeaponType].transform.localRotation = Quaternion.identity;
        }

        public void OnWeaponAttack(Transform target, FSMState state){
            _weaponDict[_currentWeaponType].OnWeaponAttack(target, state);
        }

        public void OnAim(Transform target){
            if (_weaponDict[_currentWeaponType] is BaseRangeWeapon rangeWeapon){
                rangeWeapon.OnAim(target);
            }
        }
    }
}