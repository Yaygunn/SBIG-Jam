using Audio.Events;
using FMOD.Studio;
using System.Diagnostics;

namespace Audio.Child
{
    public class CraftAudio
    {
        public CraftAudio(FModCommunication com, EventBindingSO data)
        {
            _com = com;
            _data = data;
        }
        
        public void Activate()
        {
            EventHub.Ev_CauldronStartCook += CauldronCook;
            EventHub.Ev_CauldronEndCook += CauldronEndCook;
            EventHub.Ev_CropPicked += CropPicked;
            EventHub.Ev_CookFail += CookFail;
            EventHub.Ev_ThrowInToCauldron += ThrowToCauldron;
        }

        public void DeActivate()
        {
            EventHub.Ev_CauldronStartCook -= CauldronCook;
            EventHub.Ev_CauldronEndCook -= CauldronEndCook;
            EventHub.Ev_CropPicked -= CropPicked;
            EventHub.Ev_CookFail -= CookFail;
            EventHub.Ev_ThrowInToCauldron -= ThrowToCauldron;
            
            _com.RelaeseInstance(ref _cauldronInstance);
        }

        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }

        private EventInstance _cauldronInstance;

        private void CauldronCook()
        {
            _com.PlayOneShot(_data.CauldronCook);
        }

        private void CauldronEndCook()
        {
        }

        private void CropPicked()
        {
            _com.PlayOneShot(_data.CropPicked);
        }

        private void CookFail()
        {
            _com.PlayOneShot(_data.CookFail);
        }

        private void ThrowToCauldron()
        {
            _com.PlayOneShot(_data.CropInToCauldron);
        }

        private void StartCauldronIdle()
        {
            // Start the idle
            _cauldronInstance.start();
        }

        private void PauseCauldronIdle()
        {
            // Pause idle
            _cauldronInstance.setPaused(true);
        }
        
        private void StopCauldronIdle()
        {
            // Stop idle
            _cauldronInstance.setPaused(false);
        }
    }
}