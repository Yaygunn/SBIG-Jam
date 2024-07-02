using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace Audio.Events
{
    [CreateAssetMenu(fileName = "SoundEventRef", menuName = "Scriptables/SoundEvents")]
    public class EventBindingSO : ScriptableObject
    {
        #region Music
        [field:SerializeField] public EventReference Music { get; private set; }
        #endregion
        
        #region UI
        [field: SerializeField] public EventReference UIHover { get; private set; }
        [field: SerializeField] public EventReference UIClick { get; private set; }
        #endregion
    }   
}
