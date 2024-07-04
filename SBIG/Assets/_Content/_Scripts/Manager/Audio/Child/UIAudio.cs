using Audio.Events;
using FMOD.Studio;

namespace Audio.Child
{
    public class UIAudio
    {
        public UIAudio(FModCommunication com, EventBindingSO data)
        {
            _com = com;
            _data = data;
        }

        public void Activate()
        {
            EventHub.Event_UIHover += UIHover;
            EventHub.Event_UIClick += UIClick;
            EventHub.Event_UISlider += UISlider;
        }

        public void DeActivate()
        {
            EventHub.Event_UIHover -= UIHover;
            EventHub.Event_UIClick -= UIClick;
            EventHub.Event_UISlider -= UISlider;
        }

        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }

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