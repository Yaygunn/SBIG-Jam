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
            EventHub.Ev_NoAmmo += NoAmmo;
        }

        public void DeActivate()
        {
            EventHub.Ev_ReloadStarted -= ReloadSound;
            EventHub.Ev_NoAmmo -= NoAmmo;
        }

        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }

        private void ReloadSound()
        {
            _com.PlayOneShot(_data.Reload);
        }
        private void NoAmmo()
        {
            _com.PlayOneShot(_data.NoAmmo);
        }
    }
}