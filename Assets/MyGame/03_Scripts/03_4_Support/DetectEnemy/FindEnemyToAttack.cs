using UnityEngine;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(menuName = "MyGame/Support/FindEnemyToAttack")] 
    public class FindEnemyToAttack : ScriptableObject{
        [SerializeField] private float _distance = 10f;
        public IHittedByPlayer FindEnemyByDistance(Transform finder){
            Collider[] colliders = Physics.OverlapSphere(finder.position, _distance);
            float minDistance = float.MaxValue;
            IHittedByPlayer enemy = null;
            foreach (var collider in colliders){
                IHittedByPlayer hittedByPlayer = collider.GetComponent<IHittedByPlayer>();
                if (hittedByPlayer != null){
                    float currentDistance = Vector3.Distance(finder.position, collider.transform.position);
                    if (currentDistance < minDistance){
                        minDistance = currentDistance;
                        enemy = hittedByPlayer;
                    }
                }
            }
            return enemy;
        }
    }
}