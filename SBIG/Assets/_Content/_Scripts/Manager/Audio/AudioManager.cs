using System;
using Audio;
using Audio.Child;
using Audio.Events;
using FMOD.Studio;
using FMODUnity;
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
        
        private Bus _musicBus;
        private Bus _narratorBus;
        private Bus _sfxBus;
        
        #region Volume Controls

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                SetMusicVolume(_musicVolume);
            }
        }
        
        public float NarratorVolume
        {
            get => _narratorVolume;
            set
            {
                _narratorVolume = value;
                SetNarratorVolume(_narratorVolume);
            }
        }
        
        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                SetSfxVolume(_sfxVolume);
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
            _initialMusicVolume = GetMusicVolume();
            SetMusicVolume(_initialMusicVolume * 0.4f);
        }

        private void NormalizeMusicVolume()
        {
            SetMusicVolume(_initialMusicVolume);
        }

        private void Initialize()
        {
            _fmodCommunication = new FModCommunication();

            _musicAudio = new MusicAudio(_fmodCommunication, _eventBindingSO);
            _uiAudio = new UIAudio(_fmodCommunication, _eventBindingSO);
            _combatAudio = new CombatAudio(_fmodCommunication, _eventBindingSO);
            _craftAudio = new CraftAudio(_fmodCommunication, _eventBindingSO);
            
            _musicBus = RuntimeManager.GetBus("bus:/Music");
            _narratorBus = RuntimeManager.GetBus("bus:/Narration");
            _sfxBus = RuntimeManager.GetBus("bus:/SFX");

            RegisterAudioEvents();
            
            _musicAudio.Activate();
            _uiAudio.Activate();
            _combatAudio.Activate();
            _craftAudio.Activate();
            
            // Fetch the current volume settings from FMOD
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume", GetMusicVolume());
            _narratorVolume = PlayerPrefs.GetFloat("NarrationVolume", GetNarratorVolume());
            _sfxVolume = PlayerPrefs.GetFloat("SFXVolume", GetSfxVolume());
            
            // Set the starting volume
            SetMusicVolume(_musicVolume);
            SetNarratorVolume(_narratorVolume);
            SetSfxVolume(_sfxVolume);
        }

        private void RegisterAudioEvents()
        {
            // Register any audio events we need here, like starting of the level
        }
        
        public void SetMusicVolume(float volume)
        {
            _musicBus.setVolume(volume);
        }

        public void SetNarratorVolume(float volume)
        {
            _narratorBus.setVolume(volume);
        }

        public void SetSfxVolume(float volume)
        {
            _sfxBus.setVolume(volume);
        }

        public float GetMusicVolume()
        {
            _musicBus.getVolume(out float volume);
            return volume;
        }

        public float GetNarratorVolume()
        {
            _narratorBus.getVolume(out float volume);
            return volume;
        }

        public float GetSfxVolume()
        {
            _sfxBus.getVolume(out float volume);
            return volume;
        }
    }
}
