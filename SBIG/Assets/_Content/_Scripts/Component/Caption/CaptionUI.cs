using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Components.Caption
{
    public class CaptionUI : MonoBehaviour
    {
        private TextMeshProUGUI _captionText;

        private void Awake()
        {
            _captionText = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            _captionText.text = text;
        }

        public void Destroy(float afterSeconds = 0f)
        {
            Destroy(gameObject, afterSeconds);
        }
    }   
}
