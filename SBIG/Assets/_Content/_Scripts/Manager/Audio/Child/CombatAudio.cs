using Audio.Events;
using Manager.Caption;
using Utilities;

namespace Audio.Child
{
    public class CombatAudio
    {
        private bool _reloadNarrationPlayed;
        private int _waveCount = 1;
        public CombatAudio(FModCommunication com, EventBindingSO data)
        {
            _com = com;
            _data = data;
        }
        
        public void Activate()
        {
            EventHub.Ev_ReloadStarted += ReloadSound;
            EventHub.Ev_NoAmmo += NoAmmo;
            EventHub.Ev_ReloadFinished += ReloadFinished;
            EventHub.Ev_CombatStart += WaveStarted;
            EventHub.Ev_PlayerDied += OnPlayerDied;
        }

        public void DeActivate()
        {
            EventHub.Ev_ReloadStarted -= ReloadSound;
            EventHub.Ev_NoAmmo -= NoAmmo;
            EventHub.Ev_ReloadFinished -= ReloadFinished;
            EventHub.Ev_CombatStart -= WaveStarted;
            EventHub.Ev_PlayerDied -= OnPlayerDied;
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

        private void ReloadFinished()
        {
            if (_reloadNarrationPlayed)
                return;
            
            _reloadNarrationPlayed = true;
            _com.PlayOneShot(_data.ReloadFinished);
        }

        private void WaveStarted()
        {
            switch (_waveCount)
            {
                case 1:
                    CaptionManager.Instance.StartCaption( SBIGUtils.GetEventName(_data.WaveOneStart) );
                    _com.PlayOneShot(_data.WaveOneStart);
                    break;
                case 2:
                    CaptionManager.Instance.StartCaption( SBIGUtils.GetEventName(_data.WaveTwoStart) );
                    _com.PlayOneShot(_data.WaveTwoStart);
                    break;
            }

            _waveCount++;
        }
        
        private void OnPlayerDied()
        {
            _com.PlayOneShot(_data.PlayerDeath);
        }
    }
}