using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(menuName = "MyGame/Support/ShootSilk")]
    public class ShootSilk : ScriptableObject{
        [SerializeField] private GameObject _shootSilkPrefab;
        [SerializeField] private float _startWidth = 0.1f;
        [SerializeField] private float _endWidth = 0.1f;
        [SerializeField] private Color _startColor = Color.white;
        [SerializeField] private Color _endColor = Color.white;
        
        private LineRenderer _lineRenderer;
        private SilkController _silkController;
        private Transform _origin;
        private bool _isInit = false;

        public void Init(){
            if(_lineRenderer == null) _isInit = false;
            if (_isInit) return;
            _isInit = true;
            _lineRenderer = Instantiate(_shootSilkPrefab).GetComponent<LineRenderer>();
            _silkController = _lineRenderer.gameObject.GetComponent<SilkController>();
            _lineRenderer.startWidth = _startWidth;
            _lineRenderer.endWidth = _endWidth;
            _lineRenderer.startColor = _startColor;
            _lineRenderer.endColor = _endColor;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.positionCount = 2;
        }

        public Tween ShootSilkToTarget(Vector3 startPos, Vector3 endPos, float duration)
        {
            if(!_isInit) Init();
            _silkController.UpdateSilk(_origin, endPos);

            return DOTween.To(() => startPos, x => _lineRenderer.SetPosition(1, x), endPos, duration);
        }

        public Tween ShootSilkToTarget(Transform origin, Vector3 endPos, float duration)
        {
            if(!_isInit) Init();
            _origin = origin;
            return ShootSilkToTarget(_origin.position, endPos, duration);
        }

        public void UnUseSilk(){
            _silkController.UnUseSilk();
        }
    }
}