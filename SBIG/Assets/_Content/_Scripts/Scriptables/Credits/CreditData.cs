using UnityEngine;

namespace Scriptables.Credits
{
    [CreateAssetMenu (fileName = "Credit", menuName = "Scriptables/CreditData")]
    public class CreditData : ScriptableObject
    {
        public string Position;
        public string Name;
        public Sprite Flag;
        public string ItchProfile;
    }   
}
