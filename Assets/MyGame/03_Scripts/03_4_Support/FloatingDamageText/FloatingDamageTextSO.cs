using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(fileName = "FloatingDamageTextSO", menuName = "MyGame/Support/FloatingDamageTextSO", order = 0)]
    public class FloatingDamageTextSO : BaseSOWithPool{
        public float timeToHide = 0.5f;

        public GameObject Spawn(float dame, RectTransform container){
            if(!_isInit)
                Init(1001);
            var gameObject = SpawnObject();  
            gameObject.transform.SetParent(container);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.GetComponent<FloatingDamageText>().OnSpawn(dame, this);
            return gameObject;         
        }
    }
}