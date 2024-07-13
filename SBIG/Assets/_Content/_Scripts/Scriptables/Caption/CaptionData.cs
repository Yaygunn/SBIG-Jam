using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Caption
{
    [CreateAssetMenu(fileName = "CaptionData", menuName = "Scriptables/Caption/CaptionData", order = -1)]
    public class CaptionData : ScriptableObject
    {
        [field:SerializeField] public EventReference SoundEventReferance { get; private set; }
        public string audioEventReference;
        public List<CaptionLine> CaptionLines;
        public int maximumCaptionsOnScreen = 2;
    }

    [System.Serializable]
    public struct CaptionLine
    {
        public string Caption;
        [Tooltip("How long to wait before we show the text")]
        public float StartDelay;
        [Tooltip("How long to keep the text on screen for")]
        public float Duration;
        [Tooltip("Should this caption stay on screen with the next one")]
        public bool StackWithNext;
    }
}
