using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _menuContainer;
    private VisualElement _settingsContainer;
    private VisualElement _creditsContainer;
    
    #region Menu Buttons
    private Button _playButton;
    private Button _settingsButton;
    private Button _creditsButton;
    private Button _quitButton;
    // I don't know who to hook multiple buttons to the same callback
    private Button _backButtonSettings;
    private Button _backButtonCredits;
    #endregion
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        
        _menuContainer = _document.rootVisualElement.Q("MenuContainer");
        _settingsContainer = _document.rootVisualElement.Q("SettingsContainer");
        _creditsContainer = _document.rootVisualElement.Q("CreditsContainer");
        
        _playButton = _document.rootVisualElement.Q("PlayButton") as Button;
        _settingsButton = _document.rootVisualElement.Q("SettingsButton") as Button;
        _creditsButton = _document.rootVisualElement.Q("CreditsButton") as Button;
        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _backButtonSettings = _document.rootVisualElement.Q("BackButtonSettings") as Button;
        _backButtonCredits = _document.rootVisualElement.Q("BackButtonCredits") as Button;

        RegisterButtonCallbacks();
    }

    private void RegisterButtonCallbacks()
    {
        _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        _creditsButton.RegisterCallback<ClickEvent>(OnCreditsButtonClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        _backButtonSettings.RegisterCallback<ClickEvent>(OnBackButtonClick);
        _backButtonCredits.RegisterCallback<ClickEvent>(OnBackButtonClick);
    }

    private void OnDisable()
    {
        _playButton.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
        _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClick);
        _creditsButton.UnregisterCallback<ClickEvent>(OnCreditsButtonClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        _backButtonSettings.UnregisterCallback<ClickEvent>(OnBackButtonClick);
        _backButtonCredits.UnregisterCallback<ClickEvent>(OnBackButtonClick);
    }
    
    private void OnPlayButtonClick(ClickEvent evt)
    {
        // Load game scene
        Debug.Log("Load Game Scene");
    }
    
    private void OnSettingsButtonClick(ClickEvent evt)
    {
        // Hide all other containers
        _menuContainer.style.display = DisplayStyle.None;
        _creditsContainer.style.display = DisplayStyle.None;
        
        // Show relevant container
        _settingsContainer.style.display = DisplayStyle.Flex;
    }
    
    private void OnCreditsButtonClick(ClickEvent evt)
    {
        // Hide all other containers
        _menuContainer.style.display = DisplayStyle.None;
        _settingsContainer.style.display = DisplayStyle.None;
        
        // Show relevant container
        _creditsContainer.style.display = DisplayStyle.Flex;
    }

    private void OnBackButtonClick(ClickEvent evt)
    {
        // Hide all other containers
        _settingsContainer.style.display = DisplayStyle.None;
        _creditsContainer.style.display = DisplayStyle.None;
        
        // Show relevant container
        _menuContainer.style.display = DisplayStyle.Flex;
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
