using UnityEngine;

namespace MyTools.ParticleExtendUsing{
    public interface IFactoryParticle{
        void SpawnParticle(Transform parent, Vector3 localScale = default);
        void SpawnParticle(Vector3 worldPosition, Quaternion worldRotation, Vector3 localScale = default, Transform parent = null);
        void SpawnParticle(Vector3 screenPostion, bool isUIParticle = true);
    }
}