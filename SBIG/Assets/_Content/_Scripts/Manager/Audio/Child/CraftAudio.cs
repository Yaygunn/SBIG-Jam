using Audio.Events;
using FMOD.Studio;

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
        }

        public void DeActivate()
        {
            EventHub.Ev_CauldronStartCook -= CauldronCook;
            EventHub.Ev_CauldronEndCook -= CauldronEndCook;
        }

        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }

        private EventInstance _cauldronInstance;

        private void CauldronCook()
        {
            _com.PlayOneShot( _data.CauldronCook);
        }

        private void CauldronEndCook()
        {
            
        }
    }
}