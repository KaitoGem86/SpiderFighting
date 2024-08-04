using UnityEngine;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(menuName = "MyGame/Support/FindEnemyToAttack")] 
    public class FindEnemyToAttack : ScriptableObject{
        [SerializeField] private float _distance = 10f;
        public IHitted FindEnemyByDistance(Transform finder){
            Collider[] colliders = Physics.OverlapSphere(finder.position, _distance);
            float minDistance = float.MaxValue;
            IHitted enemy = null;
            foreach (var collider in colliders){
                IHitted hittedByPlayer = collider.GetComponent<IHitted>();
                if (hittedByPlayer != null && !hittedByPlayer.IsIgnore){
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