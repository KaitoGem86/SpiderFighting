using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class ConfigTool : MonoBehaviour
    {
        [SerializeField] private StatDatasController _statDatasController;
        [SerializeField] private ConfigElementSO _configElementSO;
        [SerializeField] private Transform _container;

        private List<GameObject> _listConfigElement = new List<GameObject>();

        void Awake()
        {
            _configElementSO.Init(0, _container);
        }


        public void OnEnable()
        {
            foreach (var configElement in _listConfigElement)
            {
                _configElementSO.DespawnObject(configElement);
            }
            _listConfigElement.Clear();
            foreach (var statData in _statDatasController.StatDatas)
            {
                _listConfigElement.Add(_configElementSO.Spawn(statData));
            }
        }

    }
}