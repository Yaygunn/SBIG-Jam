using Audio.Events;
using FMOD.Studio;

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

            _com.SetInstance(ref _musicInstance, _data.Music);
        }

        public void DeActivate()
        {
            EventHub.Event_StartMenu -= OnMenu;

            _com.RelaeseInstance(ref _musicInstance);
        }
        
        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }


        EventInstance _musicInstance;

        private void OnMenu()
        {
            _com.PlayInstanceIfNotPlaying(ref _musicInstance, _data.Music);
            _com.SetParameter(ref _musicInstance, "Song" , 0);
        }
    }
}