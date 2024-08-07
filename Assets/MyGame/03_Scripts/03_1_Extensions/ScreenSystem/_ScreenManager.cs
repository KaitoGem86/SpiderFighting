using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace MyTools.ScreenSystem
{
    public class _ScreenManager : MonoBehaviour
    {
        private static _ScreenManager _instance;
        public static _ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<_ScreenManager>();
                }
                return _instance;
            }
        }

        [SerializeField] private string _screenFolderPath;
        [SerializeField] private _BaseScreen[] _screens;
        [SerializeField] private Transform _screenCanvas;
        [SerializeField] private SerializedDictionary<_ScreenTypeEnum, _BaseScreen> _screenDict = new SerializedDictionary<_ScreenTypeEnum, _BaseScreen>();
        private _ScreenTypeEnum _currentScreenType = _ScreenTypeEnum.None;
        public Transform ScreenCanvas => _screenCanvas;

#if UNITY_EDITOR
        [ContextMenu("LoadPopupPrefabs")]
        void LoadPopupPrefabs()
        {
            var lstPrefabs = new List<_BaseScreen>();
            var lstNames = System.IO.Directory.GetFiles($"{_screenFolderPath}",
                "*.prefab", System.IO.SearchOption.AllDirectories);
            foreach (var itName in lstNames)
            {
                var obj = UnityEditor.AssetDatabase.LoadAssetAtPath<_BaseScreen>($"{itName}");
                if (obj == null) continue;
                lstPrefabs.Add(obj);
            }

            _screens = lstPrefabs.ToArray();
            UnityEditor.EditorUtility.SetDirty(gameObject);
        }
#endif

        public void PreLoad(){
            foreach(var screen in _screens){
                if(_screenDict.ContainsKey(screen.ScreenType)) continue;
                var go = Instantiate(screen.gameObject, Vector3.zero, Quaternion.identity, _screenCanvas);
                go.SetActive(false);
                _screenDict.Add(screen.ScreenType, go.GetComponent<_BaseScreen>());
                go.transform.localPosition = Vector3.zero;
            }
        }

        public void ShowScreen(_ScreenTypeEnum screenType){
            if(_currentScreenType == screenType) return;
            if(_screenDict.ContainsKey(screenType) == false){
                PreLoad();
            }
            if(_currentScreenType != _ScreenTypeEnum.None){
                _screenDict[_currentScreenType].Hide();
            }
            _currentScreenType = screenType;
            _screenDict[_currentScreenType].Show();
        }

        public void HideScreen(_ScreenTypeEnum screenType){
            if(_currentScreenType != screenType || screenType == _ScreenTypeEnum.None || _currentScreenType == _ScreenTypeEnum.None)
                return;
            _screenDict[screenType].Hide();
            _currentScreenType = _ScreenTypeEnum.None;
        }

        public void HideCurrentScreen(){
            if(_currentScreenType == _ScreenTypeEnum.None)
                return;
            _screenDict[_currentScreenType].Hide();
            _currentScreenType = _ScreenTypeEnum.None;
        }

        public float GetScreenHeight(){
            return _screenCanvas.GetComponent<RectTransform>().sizeDelta.y;
        }

// #if UNITY_EDITOR
//         private void Update(){
//             if(Input.GetKeyDown(KeyCode.A)){
//                 _screenDict[_currentScreenType].Hide();
//             }
//             else if(Input.GetKeyDown(KeyCode.S)){
//                 ShowScreen(_currentScreenType);
//             }
//             if(Input.GetKeyDown(KeyCode.D)){
//                 ShowScreen(_ScreenTypeEnum.SelectPlant);
//             }
//             if(Input.GetKeyDown(KeyCode.F)){
//                 ShowScreen(_ScreenTypeEnum.Recipe);
//             }
//         }
// #endif
    }
}