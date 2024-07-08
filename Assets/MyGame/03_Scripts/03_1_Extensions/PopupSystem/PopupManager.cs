using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyTools.PopupSystem
{
    public class PopupManager : MonoBehaviour
    {
        private static PopupManager _instance;
        public static PopupManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PopupManager>();
                }
                return _instance;
            }
        }

        [SerializeField] private string _popupFolderPath;
        [SerializeField] private BasePopup[] _popups;
        [SerializeField] private Canvas _canvasContainer;
        [SerializeField] private Transform _disableContainer;
        [SerializeField] private Image _transparentBackground;
        private Dictionary<PopupTypeEnum, BasePopup> _dictCurrentPopups = new Dictionary<PopupTypeEnum, BasePopup>();
        private Stack<GameObject> _stackPopups = new Stack<GameObject>();

#if UNITY_EDITOR
        [ContextMenu("LoadPopupPrefabs")]
        void LoadPopupPrefabs()
        {
            var lstPrefabs = new List<BasePopup>();
            var lstNames = System.IO.Directory.GetFiles($"{_popupFolderPath}",
                "*.prefab", System.IO.SearchOption.AllDirectories);
            foreach (var itName in lstNames)
            {
                var obj = UnityEditor.AssetDatabase.LoadAssetAtPath<BasePopup>($"{itName}");
                if (obj == null) continue;
                lstPrefabs.Add(obj);
            }

            _popups = lstPrefabs.ToArray();
            UnityEditor.EditorUtility.SetDirty(gameObject);
        }
#endif

        private void PreLoad(){
        }

        private void ReOrder(){

        }

        private void ShowAnimPopup(PopupTypeEnum type){
            var popup = GetPopup(type);
            popup.ShowPopup();
        }

        private void ShowNotAnimPopup(PopupTypeEnum type){

        }

        private BasePopup GetPopup(PopupTypeEnum type){
            if(_dictCurrentPopups.ContainsKey(type) && !_dictCurrentPopups[type].IsShow){
                return _dictCurrentPopups[type];
            }
            else if(_dictCurrentPopups.ContainsKey(type) && _dictCurrentPopups[type].IsShow){
                throw new System.Exception("Popup is showing!");
            }
            else{
                CreateNewPopup(type);
                return _dictCurrentPopups[type];
            }
        }

        private void CreateNewPopup(PopupTypeEnum type){
            for(int i = 0; i < _popups.Length; i++){
                if(_popups[i].PopupType == type){
                    var go = Instantiate(_popups[i].gameObject, Vector3.zero, Quaternion.identity, _disableContainer.transform);
                    go.SetActive(false);
                    _dictCurrentPopups.Add(type, go.GetComponent<BasePopup>());
                    break;
                }
            }
        }
    }
}