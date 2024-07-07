using Audio.Events;

namespace Audio.Child
{
    public class CombatAudio
    {
        public CombatAudio(FModCommunication com, EventBindingSO data)
        {
            _com = com;
            _data = data;
        }
        
        public void Activate()
        {
            EventHub.Ev_ReloadStarted += ReloadSound;
        }

        public void DeActivate()
        {
            EventHub.Ev_ReloadStarted -= ReloadSound;
        }

        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }

        private void ReloadSound()
        {
            _com.PlayOneShot(_data.Reload);
        }
    }
}