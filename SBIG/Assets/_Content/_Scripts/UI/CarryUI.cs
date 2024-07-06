using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CarryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pickBackground;
        [SerializeField] private GameObject _cauldronBackground;

        [SerializeField] private TextMeshProUGUI _pickText;
        [SerializeField] private TextMeshProUGUI _cauldronText;
        void Start()
        {
            EventHub.Ev_ShowPickableText += ShowPickText;
            EventHub.Ev_ShowCookInCauldronText += ShowCookInCauldron;
            EventHub.Ev_CloseCarryTexts += CloseTexts;
        }

        private void OnDestroy()
        {
            EventHub.Ev_ShowPickableText -= ShowPickText;
            EventHub.Ev_ShowCookInCauldronText -= ShowCookInCauldron;
            EventHub.Ev_CloseCarryTexts -= CloseTexts;
        }

        private void ShowPickText(string text, bool isPickable)
        {
            if(isPickable)
            {
                text += " [E]";
            }
            _pickBackground.SetActive(isPickable);
            _pickText.text = text;
        }

        private void ShowCookInCauldron(string text)
        {
            _cauldronBackground.SetActive(true);
            _cauldronText.text = "cook [R]";
        }

        private void CloseTexts()
        {
            _cauldronBackground.SetActive(false);
            _pickBackground.SetActive(false);
        }
    }   
}
