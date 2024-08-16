using MyTools.Event;
using UnityEngine;

namespace Collectible
{
    public class ActiveScanEffect : MonoBehaviour
    {
        [SerializeField] private BoolEvent _onActiveScan;

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {
                Debug.Log("OnTriggerEnter " + other.name);
                _onActiveScan?.Raise(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _onActiveScan?.Raise(false);
            }
        }
    }
}