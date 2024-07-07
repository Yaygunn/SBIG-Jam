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
        [field:SerializeField] public EventReference MenuMusic { get; private set; }

        [field:SerializeField] public EventReference LevelMusic { get; private set; }

        [field:SerializeField] public EventReference Ambiance { get; private set; }
        
        #endregion
        
        #region UI
        [field: SerializeField] public EventReference UIHover { get; private set; }
        [field: SerializeField] public EventReference UIClick { get; private set; }
        [field: SerializeField] public EventReference UISlider { get; private set; }
        #endregion

        #region Weapon

        [field: SerializeField] public EventReference Reload { get; private set; }

        #endregion

        #region Craft

        [field: SerializeField] public EventReference CauldronCook { get; private set; }

        #endregion
    }
}
