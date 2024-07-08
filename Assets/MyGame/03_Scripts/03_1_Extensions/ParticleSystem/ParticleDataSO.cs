using Core.SystemGame.Factory;
using UnityEngine;

namespace MyTools.ParticleExtendUsing
{
    [CreateAssetMenu(fileName = "ParticleDataSO", menuName = "MyTools/ParticleDataSO", order = 1)]
    public class ParticleDataSO : BaseSOWithPool, IFactoryParticle
    {
        [SerializeField] private float _timeToEnd = 0f;
        public void SpawnParticle(Transform parent, Vector3 localScale = default)
        {
            var go = SpawnObject();
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = localScale == default ? Vector3.one : localScale;
            var tmp = go.AddComponent<AutoEventForParticle>();
            var tempParticle = go.GetComponent<ParticleSystem>();
            tmp.Init(this, tempParticle.main.loop ? _timeToEnd : tempParticle.main.duration);
            go.SetActive(true);
            tmp.OnStart();
        }
        public void SpawnParticle(Vector3 worldPosition, Quaternion worldRotation, Vector3 localScale = default, Transform parent = null)
        {
            var go = SpawnObject();
            go.transform.position = worldPosition;
            go.transform.rotation = worldRotation;
            go.transform.localScale = localScale == default ? Vector3.one : localScale;
            go.transform.SetParent(parent);
            var tmp = go.AddComponent<AutoEventForParticle>();
            var tempParticle = go.GetComponent<ParticleSystem>();
            tmp.Init(this, tempParticle.main.loop ? _timeToEnd : tempParticle.main.duration);
            go.SetActive(true);
            tmp.OnStart();
        }
        public void SpawnParticle(Vector3 screenPostion, bool isUIParticle = true)
        {
            // var go = SpawnObject();
            // go.transform.position = screenPostion;
            // go.SetActive(true);
        }
    }
}