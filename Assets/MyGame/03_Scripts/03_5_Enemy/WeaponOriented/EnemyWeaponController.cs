using System.Collections.Generic;
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
        }

        public void OnWeaponAttack(Transform target){
            _weaponDict[_currentWeaponType].OnWeaponAttack(target);
        }
    }
}