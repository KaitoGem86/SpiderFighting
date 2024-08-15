using Core.GamePlay.Support;
using UnityEngine;

namespace Core.GamePlay.Enemy
{
    public class HandWeapon : BaseWeaponOriented
    {
        [SerializeField] LayerMask _checkLayer;
        [SerializeField] private float _range;
        public override void OnWeaponAttack(Transform target)
        {
            // _collider.enabled = true;
            // Debug.DrawLine(transform.position, target.position, Color.red, 100);
            // if(Vector3.Distance(transform.position, target.position) <= 0.5f)
            // {
            //     Debug.Log("HandWeapon Attack " + target.name);
            // }
            var hit = Physics.OverlapSphere(transform.position, _range, _checkLayer);
            if(hit.Length > 0)
            {
                foreach (var item in hit)
                {
                    if(item.CompareTag("Player"))
                    {
                        item.GetComponent<IHitted>().HittedByPlayer(Extensions.SystemGame.AIFSM.FSMState.Hit);
                    }
                }
            }
        }

        // public void OnTriggerEnter(Collider collider){
        //     if (collider.CompareTag("Player"))
        //     {
        //         collider.GetComponent<IHitted>().HittedByPlayer(Extensions.SystemGame.AIFSM.FSMState.Hit);
        //         _collider.enabled = false;
        //     }
            
        // }
    }
}