using Core.GamePlay.Support;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class BaseRangeWeapon : BaseWeaponOriented
    {
        [SerializeField] protected LineRenderer _lineRenderer;
        [SerializeField] protected WebBulletSO _bulletSO;
        [SerializeField] protected int _numberBullet;
        [SerializeField] protected Transform _muzzle;
        protected bool _onAim = false;
        protected Transform _target;

        public virtual void Update()
        {
            if (!_onAim)
            {
                return;
            }
            _lineRenderer.SetPositions(new[] { _muzzle.position, _target.position + Vector3.up });
        }

        public virtual void OnAim(Transform target)
        {
            _onAim = true;
            _target = target;
        }

        public override void OnWeaponAttack(Transform target)
        {
            _onAim = false;
            _lineRenderer.SetPositions(new[] { Vector3.zero, Vector3.zero });
            for (int i = 0; i < _numberBullet; i++)
            {
                _bulletSO.Spawn(_muzzle, target.position + Vector3.up - _muzzle.position);
            }
        }
    }
}