using FMODUnity;

namespace Utilities
{
    // Had to rename the class to Utils otherwise it clashes
    public static class SBIGUtils
    {
        public static EventReference GetEventName(EventReference referance)
        {
            return referance;
            /*string referenceString = referance.ToString();
            int startIndex = referenceString.IndexOf("event:/Narration/") + "event:/Narration/".Length;
            if (startIndex > "event:/Narration/".Length - 1 && startIndex < referenceString.Length)
            {
                string eventName = referenceString.Substring(startIndex);
                if (eventName.EndsWith(")"))
                {
                    eventName = eventName.Substring(0, eventName.Length - 1);
                }
                return eventName;
            }
            return string.Empty;*/
        }
    }   
}
