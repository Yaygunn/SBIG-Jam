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
        [field: SerializeField] public EventReference ReloadFinished { get; private set; }
        #endregion

        #region Craft

        [field: SerializeField] public EventReference CauldronCook { get; private set; }

        [field: SerializeField] public EventReference CropPicked { get; private set; }

        [field: SerializeField] public EventReference CropInToCauldron { get; private set; }

        [field: SerializeField] public EventReference CookFail{ get; private set; }
        [field: SerializeField] public EventReference NoAmmo{ get; private set; }

        #endregion
        
        #region Level Mechanics
        [field: SerializeField] public EventReference WaveOneStart { get; private set; }
        [field: SerializeField] public EventReference WaveTwoStart { get; private set; }
        #endregion
        
        #region Player
        [field: SerializeField] public EventReference PlayerDeath { get; private set; }
        #endregion
    }
}
