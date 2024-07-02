using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuEvents : MonoBehaviour
{
    private UIDocument _document;
    
    #region Menu Buttons
    private Button _playButton;
    private Button _settingsButton;
    private Button _creditsButton;
    private Button _quitButton;
    #endregion
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        
        _playButton = _document.rootVisualElement.Q("PlayButton") as Button;
        _settingsButton = _document.rootVisualElement.Q("SettingsButton") as Button;
        _creditsButton = _document.rootVisualElement.Q("CreditsButton") as Button;
        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;

        RegisterButtonCallbacks();
    }

    private void RegisterButtonCallbacks()
    {
        _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        _creditsButton.RegisterCallback<ClickEvent>(OnCreditsButtonClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
    }

    private void OnDisable()
    {
        _playButton.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
        _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClick);
        _creditsButton.UnregisterCallback<ClickEvent>(OnCreditsButtonClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
    }
    
    private void OnPlayButtonClick(ClickEvent evt)
    {
        // Load game scene
        Debug.Log("Load Game Scene");
    }
    
    private void OnSettingsButtonClick(ClickEvent evt)
    {
        // Open settings menu
        Debug.Log("Settings Menu");
    }
    
    private void OnCreditsButtonClick(ClickEvent evt)
    {
        // Open credits menu
        Debug.Log("Credits Menu");
    }
    
    private void OnQuitButtonClick(ClickEvent evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
