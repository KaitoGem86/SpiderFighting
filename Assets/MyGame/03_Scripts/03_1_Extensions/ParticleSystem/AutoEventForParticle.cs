using System.Collections;
using UnityEngine;

namespace MyTools.ParticleExtendUsing{
    public class AutoEventForParticle : MonoBehaviour{
        private ParticleDataSO _soManager;
        private float _duration;
        private bool _isInit = false;

        public void OnStart(){
            StartCoroutine(OnParticleComplete(_duration));
        }

        private IEnumerator OnParticleComplete(float duration){
            yield return new WaitForSeconds(duration);
            _soManager.DespawnObject(gameObject);
        }

        public void Init(ParticleDataSO soManager, float duration){
            if(_isInit) return;
            _isInit = true;
            _soManager = soManager;
            _duration = duration;
        }
    }
}