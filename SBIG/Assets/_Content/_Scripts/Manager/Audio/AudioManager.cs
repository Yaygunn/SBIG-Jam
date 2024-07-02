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
        }

        private void RegisterAudioEvents()
        {
            // Register any audio events we need here, like starting of the level
        }
    }
}
