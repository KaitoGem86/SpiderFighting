using UnityEngine;

namespace Core.GamePlay.Support
{
    public class SilkController : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private Transform _origin;
        private Vector3 _endPos;

        public void UpdateSilk(Transform origin, Vector3 endPos)
        {
            _origin = origin;
            _endPos = endPos;
            _lineRenderer.SetPosition(0, _origin.position);
            _lineRenderer.SetPosition(1, endPos);
        }

        public void LateUpdate()
        {
            if (_origin == null) return;
            _lineRenderer.SetPosition(0, _origin.position);
            _lineRenderer.SetPosition(1, _endPos);
        }

        public void UnUseSilk(){
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
            _origin = null;
        }
    }
}