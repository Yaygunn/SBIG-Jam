using System.Collections;
using System.Collections.Generic;
using Components.Caption;
using FMODUnity;
using Scriptables.Caption;
using TMPro;
using UnityEngine;
using Utilities.Singleton;

namespace Manager.Caption
{
    public class CaptionManager : Singleton<CaptionManager>
    {
        [SerializeField] private GameObject captionUIHolder;
        [SerializeField] private GameObject captionUITextPrefab;
        [SerializeField] private CaptionData activeCaption;
        
        private Dictionary<EventReference, CaptionData> captionDatabase = new Dictionary<EventReference, CaptionData>();
        private Coroutine displayCaptionCoroutine = null;
        
        void Start()
        {
            SetupCaptions();
            HideCaptions();
        }

        private void SetupCaptions()
        {
            CaptionData[] allCaptions = Resources.LoadAll<CaptionData>("Captions");

            if (allCaptions.Length > 0)
            {
                foreach (CaptionData caption in allCaptions)
                {
                    captionDatabase.Add(caption.SoundEventReferance, caption);
                }
            }
        }

        public void StartCaption(EventReference captionReference)
        {
            if (captionDatabase.TryGetValue(captionReference, out var captionToStart))
            {
                if (captionToStart.CaptionLines.Count > 0)
                {
                    // if there is an active caption, stop it
                    if (displayCaptionCoroutine != null)
                    {
                        StopCoroutine(displayCaptionCoroutine);
                        displayCaptionCoroutine = null;
                        activeCaption = null;
                        ClearActiveCaptions();
                    }
                    
                    activeCaption = captionToStart;
                    displayCaptionCoroutine = StartCoroutine(DisplayCaption());
                }
            }
            else
            {
                Debug.Log($"(CaptionManager) Caption called, but not found: {captionReference}");
            }
        }

        private IEnumerator DisplayCaption()
        {
            ShowCaptions();
            int captionCount = 0;
            
            foreach (CaptionLine captionLine in activeCaption.CaptionLines)
            {
                yield return new WaitForSeconds(captionLine.StartDelay);
                
                if ( captionCount >= activeCaption.maximumCaptionsOnScreen)
                {
                    Destroy(captionUIHolder.transform.GetChild(0).gameObject);
                    captionCount--;
                }
                
                GameObject captionText = Instantiate(captionUITextPrefab, captionUIHolder.transform);
                CaptionUI captionUI = captionText.GetComponent<CaptionUI>();
                captionUI.SetText(captionLine.Caption);
                captionCount++;
                
                yield return new WaitForSeconds(captionLine.Duration);
                
                if (captionLine.StackWithNext)
                {
                    continue;
                }
                
                captionUI.Destroy();
                captionCount--;
            }
            
            HideCaptions();
            activeCaption = null;
            displayCaptionCoroutine = null;
        }

        private void ClearActiveCaptions()
        {
            foreach (Transform child in captionUIHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void UpdateCaption(string caption)
        {
            // do nothing
        }

        private void ShowCaptions()
        {
            captionUIHolder.gameObject.SetActive(true);
        }

        private void HideCaptions()
        {
            captionUIHolder.gameObject.SetActive(false);
        }
    }   
}
