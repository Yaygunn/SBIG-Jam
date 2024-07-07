using Audio.Events;
using FMOD.Studio;
using Managers.LevelStart;

namespace Audio.Child
{
    public class MusicAudio
    {
        public MusicAudio(FModCommunication com, EventBindingSO data)
        {
            _com = com;
            _data = data;
        }
        
        public void Activate()
        {
            EventHub.Event_StartMenu += OnMenu;
            EventHub.Ev_StartMusic += PlayMusic;

        }

        public void DeActivate()
        {
            EventHub.Event_StartMenu -= OnMenu;
            EventHub.Ev_StartMusic -= PlayMusic;

            _com.RelaeseInstance(ref _musicInstance);
        }
        
        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }


        EventInstance _musicInstance;

        private void OnMenu()
        {
            _com.SetInstanceAndPlay(ref _musicInstance, _data.MenuMusic);
        }

        private void PlayMusic(EStartMusic musicType)
        {
            if(musicType == EStartMusic.level)
                _com.SetInstanceAndPlay(ref _musicInstance, _data.LevelMusic);
            else
                _com.SetInstanceAndPlay(ref _musicInstance, _data.MenuMusic);
        }
    }
}