using System.Collections;
using UnityEngine;

namespace MyTools.Sound
{
    public class MySoundManager : MonoBehaviour
    {
        private static MySoundManager _instance;

        public static MySoundManager Instance {
            get {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MySoundManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("MySoundManager");
                        _instance = go.AddComponent<MySoundManager>();
                    }
                }
                return _instance;
            }
        }

        [SerializeField] private bool _isTurnOnSound = true;
        [SerializeField] private bool _isTurnOnMusic = true;
        //[SerializeField] private string _soundSettingId = "SETTING_SOUND";
        //[SerializeField] private string _musicSettingId = "SETTING_MUSIC";

        [SerializeField] private AudioSource _soundAudioSource;
        [SerializeField] private AudioSource _musicAudioSource;
        [Header("Resources")]
        [SerializeField] private AudioClip[] _turnOnFire;
        [SerializeField] private AudioClip[] _completeInteract;
        [SerializeField] private AudioClip[] _startInteract;
        [SerializeField] private AudioClip[] _cuttingTree;
        [SerializeField] private AudioClip[] _loseSounds;
        [SerializeField] private AudioClip[] _clickUIButtonSounds;
        [SerializeField] private AudioClip[] _backgroundMusics;
        [SerializeField] private AudioClip[] _fixingFarm;
        [SerializeField] private AudioClip[] _plantTree;

        private AudioClip _currentBackgroundMusic = null;
        //private int _currentBackgroundMusicRequestIndex = -1;
        public static float defaultPitch = 1f;

        /// <summary>
        /// Play sound with type and default configured value
        /// </summary>
        public void PlaySound(_SoundType type)
        {
            if(!_isTurnOnSound) return;
            var audioClip = GetAudioClip(type);
            if (audioClip == null) return;
            _soundAudioSource.PlayOneShot(audioClip);
        }

        /// <summary>
        /// Play sound with type and pitch
        /// </summary>
        public void PlaySound(_SoundType type, float pitch)
        {
            if(!_isTurnOnSound) return;
            var audioClip = GetAudioClip(type);
            if (audioClip == null) return;
            _soundAudioSource.PlayOneShot(audioClip);

        }

        public void StopSound(){
            _soundAudioSource.Stop();
        }

        public void PlaySoundPitchIncrease(_SoundType type, bool isComplete = false)
        {
            if(!_isTurnOnSound) return;
            var audioClip = GetAudioClip(type);
            if (audioClip == null) return;
            _soundAudioSource.PlayOneShot(audioClip);
            defaultPitch += 0.2f;
            if (isComplete)
                defaultPitch = 1f;
        }
        public void PlayMusic()
        {
            if(!_isTurnOnMusic) return;
            StopMusic();
            _currentBackgroundMusic = _backgroundMusics[Random.Range(0, _backgroundMusics.Length)];
            //_currentBackgroundMusicRequestIndex = 0;
            _musicAudioSource.clip = _currentBackgroundMusic;
            _musicAudioSource.Play();
            StartCoroutine(WaitAndPlayMusic(_currentBackgroundMusic.length));
        }

        public void StopMusic()
        {
            if (_currentBackgroundMusic != null)
                _musicAudioSource.Stop();
        }

        private AudioClip GetAudioClip(_SoundType type)
        {
            return type switch
            {
                _SoundType.TurnOnFire => _turnOnFire[Random.Range(0, _turnOnFire.Length)],
                _SoundType.CompleteInteract => _completeInteract[Random.Range(0, _completeInteract.Length)],
                _SoundType.StartInteract => _startInteract[Random.Range(0, _startInteract.Length)],
                _SoundType.CuttingTree => _cuttingTree[Random.Range(0, _cuttingTree.Length)],
                _SoundType.Lose => _loseSounds[Random.Range(0, _loseSounds.Length)],
                _SoundType.ClickUIButton => _clickUIButtonSounds[Random.Range(0, _clickUIButtonSounds.Length)],
                _SoundType.FixingFarm => _fixingFarm[Random.Range(0, _fixingFarm.Length)],
                _SoundType.PlantTree => _plantTree[Random.Range(0, _plantTree.Length)],
                _ => null
            };
        }

        public bool IsTurnOnSound
        {
            get => _isTurnOnSound;
            set
            {
                _isTurnOnSound = value;
            }
        }

        public bool IsTurnOnMusic
        {
            get => _isTurnOnMusic;
            set
            {
                _isTurnOnMusic = value;
            }
        }

        private IEnumerator WaitAndPlayMusic(float time)
        {
            yield return new WaitForSeconds(time);
            PlayMusic();
        }
    }

    public enum _SoundType
    {
        TurnOnFire,
        CompleteInteract,
        StartInteract,
        CuttingTree,
        Lose,
        ClickUIButton,
        TapBooster,
        FixingFarm,
        PlantTree,
        Explode,
        Coin,
        BackgroundMusic
    }
}