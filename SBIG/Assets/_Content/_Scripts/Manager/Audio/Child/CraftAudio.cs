using Audio.Events;

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
            
        }

        public void DeActivate()
        {
            
        }
        
        private FModCommunication _com { get; }

        private EventBindingSO _data { get; }
    }
}