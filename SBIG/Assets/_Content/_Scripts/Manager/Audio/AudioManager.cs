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
        
        #region Volume Controls

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                // UPDATE FMOD
            }
        }
        
        public float NarratorVolume
        {
            get => _narratorVolume;
            set
            {
                _narratorVolume = value;
                // UPDATE FMOD
            }
        }
        
        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                // UPDATE FMOD
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

        private void Initialize()
        {
            _fmodCommunication = new FModCommunication();
            _musicAudio = new MusicAudio(_fmodCommunication, _eventBindingSO);

            RegisterAudioEvents();
            
            _musicAudio.Activate();
            
            _musicVolume = FModCommunication.GetMusicVolume();
            _narratorVolume = FModCommunication.GetNarratorVolume();
            _sfxVolume = FModCommunication.GetSfxVolume();
        }

        private void RegisterAudioEvents()
        {
            // Register any audio events we need here, like starting of the level
        }
    }
}
