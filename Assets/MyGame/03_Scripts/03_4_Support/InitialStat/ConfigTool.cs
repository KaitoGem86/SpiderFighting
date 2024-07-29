using UnityEngine;

namespace Core.GamePlay.Support
{
    public class ConfigTool : MonoBehaviour
    {
        [SerializeField] private StatDatasController _statDatasController;
        [SerializeField] private ConfigElementSO _configElementSO;
        [SerializeField] private Transform _container;

        void Awake()
        {
            _configElementSO.Init(0, _container);
        }


        public void OnEnable()
        {
            foreach (var statData in _statDatasController.StatDatas)
            {
                _configElementSO.Spawn(statData);
            }
        }

    }
}