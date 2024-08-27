using System.Collections;
using MyTools.ScreenSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI{
    public class LoadingScreen : _BaseScreen{
        [SerializeField] private Image _loadingBar;
        [SerializeField] private TMP_Text _loadingText;
        private float _loadingPercent = 0f;

        protected override void OnCompleteShowItSelf()
        {
            base.OnCompleteShowItSelf();
            _loadingPercent = 0f;
            StartCoroutine(Loading());
        }

        private IEnumerator Loading(){
            while(_loadingPercent < 1f){
                _loadingPercent += Time.deltaTime * 0.2f;
                _loadingBar.fillAmount = _loadingPercent;
                _loadingText.text = $"{Mathf.RoundToInt(_loadingPercent * 100)}%";
                yield return null;
            }
            _loadingBar.fillAmount = 1f;
            SceneManager.LoadScene("Gameplay");
        }       
    }
}