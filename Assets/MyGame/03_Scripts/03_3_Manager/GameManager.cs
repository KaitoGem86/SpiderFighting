using Core.GamePlay.Enemy;
using Core.GamePlay.MyPlayer;
using MyTools.ScreenSystem;
using MyTools.Sound;
using TerrainScannerDEMO;
using UnityEngine;

namespace Core.Manager {
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EnemyGroupSO _enemyGroupSO;
        
        public static GameManager Instance { get; private set; }

        private void Awake(){
            Application.targetFrameRate = 60;
            Instance = this;
        }

        private void Start(){
            MySoundManager.Instance.PlayMusic();
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.GamePlay);
            _enemyGroupSO.Init();
        }

        private void OnApplicationQuit(){
            playerBlackBoard.PlayerData.SaveData();
            _enemyGroupSO.Dispose();
        }

        private void Update(){
            if(Input.GetKeyDown(KeyCode.F)){
                FocusOnPlayer(true);
            }
            if(Input.GetKeyUp(KeyCode.F)){
                FocusOnPlayer(false);
            }
        }

        public void FocusOnPlayer(bool value){
            Time.timeScale = value ? 0.3f : 1;
        }

        public SensorDetector detector;
        public Camera playerCamera;
        public PlayerBlackBoard playerBlackBoard;

    }
}