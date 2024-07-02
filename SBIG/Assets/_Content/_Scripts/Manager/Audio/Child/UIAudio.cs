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
            EventHub.Event_UIClick += UIBack;
        }

        public void DeActivate()
        {
            EventHub.Event_UIHover -= UIHover;
            EventHub.Event_UIClick -= UIBack;
        }

        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }

        private void UIHover()
        {
            _com.PlayOneShot(_data.UIHover);
        }
        private void UIBack()
        {
            _com.PlayOneShot(_data.UIClick);
        }
    }
}