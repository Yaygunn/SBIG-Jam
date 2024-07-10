using System;
using Audio;
using Audio.Child;
using Audio.Events;
using UnityEngine;

namespace Manager.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private EventBindingSO _eventBindingSO;

        private FModCommunication _fmodCommunication;
        private MusicAudio _musicAudio;
        private UIAudio _uiAudio;
        private CombatAudio _combatAudio;
        private CraftAudio _craftAudio;

        private float _initialMusicVolume;
        
        #region Volume Controls

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                _fmodCommunication.SetMusicVolume(_musicVolume);
            }
        }
        
        public float NarratorVolume
        {
            get => _narratorVolume;
            set
            {
                _narratorVolume = value;
                _fmodCommunication.SetNarratorVolume(_narratorVolume);
            }
        }
        
        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                _fmodCommunication.SetSfxVolume(_sfxVolume);
            }
        }
        
        private float _musicVolume;
        private float _narratorVolume;
        private float _sfxVolume;
        #endregion
        
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                
                Initialize();
            }
        }

        private void OnEnable()
        {
            EventHub.Ev_ReduceMusicVolume += LowerMusicVolume;
            EventHub.Ev_NormalizeMusicVolume += NormalizeMusicVolume;
        }

        private void OnDisable()
        {
            EventHub.Ev_ReduceMusicVolume -= LowerMusicVolume;
            EventHub.Ev_NormalizeMusicVolume -= NormalizeMusicVolume;
        }
        
        private void LowerMusicVolume()
        {
            _initialMusicVolume = _fmodCommunication.GetMusicVolume();
            _fmodCommunication.SetMusicVolume(_initialMusicVolume * 0.4f);
        }

        private void NormalizeMusicVolume()
        {
            _fmodCommunication.SetMusicVolume(_initialMusicVolume);
        }

        private void Initialize()
        {
            _fmodCommunication = new FModCommunication();

            _musicAudio = new MusicAudio(_fmodCommunication, _eventBindingSO);
            _uiAudio = new UIAudio(_fmodCommunication, _eventBindingSO);
            _combatAudio = new CombatAudio(_fmodCommunication, _eventBindingSO);
            _craftAudio = new CraftAudio(_fmodCommunication, _eventBindingSO);

            RegisterAudioEvents();
            
            _musicAudio.Activate();
            _uiAudio.Activate();
            _combatAudio.Activate();
            _craftAudio.Activate();
            
            // Fetch the current volume settings from FMOD
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume", _fmodCommunication.GetMusicVolume());
            _narratorVolume = PlayerPrefs.GetFloat("NarrationVolume", _fmodCommunication.GetNarratorVolume());
            _sfxVolume = PlayerPrefs.GetFloat("SFXVolume", _fmodCommunication.GetSfxVolume());
            
            // Set the starting volume
            _fmodCommunication.SetMusicVolume(_musicVolume);
            _fmodCommunication.SetNarratorVolume(_narratorVolume);
            _fmodCommunication.SetSfxVolume(_sfxVolume);
        }

        private void RegisterAudioEvents()
        {
            // Register any audio events we need here, like starting of the level
        }
    }
}
