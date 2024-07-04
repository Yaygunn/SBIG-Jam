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
            EventHub.Event_UIHover += UIHover;
            EventHub.Event_UIClick += UIClick;
            EventHub.Event_UISlider += UISlider;
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
        
        private void UIHover()
        {
            _com.PlayOneShot(_data.UIHover);
        }
        private void UIClick()
        {
            _com.PlayOneShot(_data.UIClick);
        }
        private void UISlider()
        {
            _com.PlayOneShot(_data.UISlider);
        }
    }
}