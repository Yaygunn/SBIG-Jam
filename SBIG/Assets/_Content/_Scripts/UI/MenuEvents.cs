using System;
using System.Collections.Generic;
using Manager.Audio;
using Managers.LevelStart;
using Scriptables.Credits;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class MenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _menuContainer;
    private VisualElement _settingsContainer;
    private VisualElement _creditsContainer;
    
    [SerializeField] VisualTreeAsset _creditItemTemplate;
    [SerializeField] private List<CreditData> _creditDataList = new List<CreditData>();
    private ListView _creditsListView;
    
    #region Menu Buttons
    private Button _playButton;
    private Button _settingsButton;
    private Button _creditsButton;
    private Button _quitButton;
    // I don't know who to hook multiple buttons to the same callback
    private Button _backButtonSettings;
    private Button _backButtonCredits;
    
    private Slider _musicSlider;
    private Slider _narratorSlider;
    private Slider _sfxSlider;

    private Slider _mouseSensitivitySlider;
    
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

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        EventHub.StartMenu();
    }

    private void RegisterButtonCallbacks()
    {
        _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        _creditsButton.RegisterCallback<ClickEvent>(OnCreditsButtonClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        _backButtonSettings.RegisterCallback<ClickEvent>(OnBackButtonClick);
        _backButtonCredits.RegisterCallback<ClickEvent>(OnBackButtonClick);
        
        _playButton.RegisterCallback<MouseEnterEvent>(OnButtonHover);
        _settingsButton.RegisterCallback<MouseEnterEvent>(OnButtonHover);
        _creditsButton.RegisterCallback<MouseEnterEvent>(OnButtonHover);
        _quitButton.RegisterCallback<MouseEnterEvent>(OnButtonHover);
        _backButtonSettings.RegisterCallback<MouseEnterEvent>(OnButtonHover);
        _backButtonCredits.RegisterCallback<MouseEnterEvent>(OnButtonHover);
    }

    private void OnDisable()
    {
        _playButton.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
        _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClick);
        _creditsButton.UnregisterCallback<ClickEvent>(OnCreditsButtonClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        _backButtonSettings.UnregisterCallback<ClickEvent>(OnBackButtonClick);
        _backButtonCredits.UnregisterCallback<ClickEvent>(OnBackButtonClick);
        
        _playButton.UnregisterCallback<MouseEnterEvent>(OnButtonHover);
        _settingsButton.UnregisterCallback<MouseEnterEvent>(OnButtonHover);
        _creditsButton.UnregisterCallback<MouseEnterEvent>(OnButtonHover);
        _quitButton.UnregisterCallback<MouseEnterEvent>(OnButtonHover);
        _backButtonSettings.UnregisterCallback<MouseEnterEvent>(OnButtonHover);
        _backButtonCredits.UnregisterCallback<MouseEnterEvent>(OnButtonHover);
        
        _musicSlider?.UnregisterCallback<PointerDownEvent>(OnSliderPointerDown);
        _narratorSlider?.UnregisterCallback<PointerDownEvent>(OnSliderPointerDown);
        _sfxSlider?.UnregisterCallback<PointerDownEvent>(OnSliderPointerDown);
        
        _musicSlider?.UnregisterCallback<ChangeEvent<float>>(OnSliderChangeEvent_music);
        _narratorSlider?.UnregisterCallback<ChangeEvent<float>>(OnSliderChangeEvent_narrator);
        _sfxSlider?.UnregisterCallback<ChangeEvent<float>>(OnSliderChangeEvent_sfx);
        
        _mouseSensitivitySlider?.UnregisterCallback<ChangeEvent<float>>(OnMouseSensitivityChange);
    }
    
    private void OnPlayButtonClick(ClickEvent evt)
    {
        EventHub.UIClick();
        
        SceneManager.LoadScene("GameLevel");
    }
    
    private void OnSettingsButtonClick(ClickEvent evt)
    {
        EventHub.UIClick();
        
        // Hide all other containers
        _menuContainer.style.display = DisplayStyle.None;
        _creditsContainer.style.display = DisplayStyle.None;
        
        // Show relevant container
        _settingsContainer.style.display = DisplayStyle.Flex;
        
        _settingsContainer.schedule.Execute(() =>
        {
            _musicSlider = _settingsContainer.Q<Slider>("MusicSlider");
            _narratorSlider = _settingsContainer.Q<Slider>("NarratorSlider");
            _sfxSlider = _settingsContainer.Q<Slider>("SfxSlider");
            _mouseSensitivitySlider = _settingsContainer.Q<Slider>("MouseSensitivitySlider");
            
            _musicSlider.RegisterCallback<PointerDownEvent>(OnSliderPointerDown);
            _narratorSlider.RegisterCallback<PointerDownEvent>(OnSliderPointerDown);
            _sfxSlider.RegisterCallback<PointerDownEvent>(OnSliderPointerDown);
            
            _musicSlider.RegisterCallback<ChangeEvent<float>>(OnSliderChangeEvent_music);
            _narratorSlider.RegisterCallback<ChangeEvent<float>>(OnSliderChangeEvent_narrator);
            _sfxSlider.RegisterCallback<ChangeEvent<float>>(OnSliderChangeEvent_sfx);
            
            _mouseSensitivitySlider.RegisterCallback<ChangeEvent<float>>(OnMouseSensitivityChange);
            _mouseSensitivitySlider.value = LevelStartManager.Instance.GetMouseSensitivity();
            
            // Update initial slider values
            _musicSlider.value = AudioManager.Instance.MusicVolume;
            _narratorSlider.value = AudioManager.Instance.NarratorVolume;
            _sfxSlider.value = AudioManager.Instance.SfxVolume;
        }).StartingIn(0);
    }

    private void OnMouseSensitivityChange(ChangeEvent<float> evt)
    {
        LevelStartManager.Instance.SetMouseSensitivity(evt.newValue);
    }

    private void OnCreditsButtonClick(ClickEvent evt)
    {
        EventHub.UIClick();
        
        // Hide all other containers
        _menuContainer.style.display = DisplayStyle.None;
        _settingsContainer.style.display = DisplayStyle.None;
        
        // Show relevant container
        _creditsContainer.style.display = DisplayStyle.Flex;
        
        InitializeCreditsListView();
    }

    private void InitializeCreditsListView()
    {
        if (_creditsListView == null)
        {
            _creditsListView = _creditsContainer.Q<ListView>("creditsListView");

            if (_creditsListView != null)
            {
                _creditsListView.itemsSource = _creditDataList;
                _creditsListView.makeItem = MakeItem;
                _creditsListView.bindItem = BindItem;
                _creditsListView.fixedItemHeight = 80;
            }
        }
    }

    VisualElement MakeItem()
    {
        return _creditItemTemplate.CloneTree();
    }

    void BindItem(VisualElement element, int index)
    {
        var positionLabel = element.Q<Label>("Position");
        var nameLabel = element.Q<Label>("Name");
        var flagImage = element.Q<VisualElement>("Flag");

        var creditData = _creditDataList[index];

        positionLabel.text = creditData.Position;
        nameLabel.text = creditData.Name;
        
        if (!string.IsNullOrEmpty(creditData.ItchProfile))
        {
            element.RegisterCallback<ClickEvent>(evt =>
            {
                EventHub.UIClick();
                Application.OpenURL(creditData.ItchProfile);
            });
        }
        
        flagImage.style.backgroundImage = new StyleBackground(creditData.Flag);
    }

    private void OnBackButtonClick(ClickEvent evt)
    {
        EventHub.UIClick();
        
        // Hide all other containers
        _settingsContainer.style.display = DisplayStyle.None;
        _creditsContainer.style.display = DisplayStyle.None;
        
        // Show relevant container
        _menuContainer.style.display = DisplayStyle.Flex;
    }
    
    private void OnSliderPointerDown(PointerDownEvent evt)
    {
        EventHub.UISlider();
    }
    
    private void OnSliderChangeEvent_music(ChangeEvent<float> evt)
    {
        float volume = evt.newValue;

        AudioManager.Instance.MusicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    
    private void OnSliderChangeEvent_narrator(ChangeEvent<float> evt)
    {
        float volume = evt.newValue;
        
        AudioManager.Instance.NarratorVolume = volume; 
        PlayerPrefs.SetFloat("NarrationVolume", volume);
    }
    
    private void OnSliderChangeEvent_sfx(ChangeEvent<float> evt)
    {
        float volume = evt.newValue;
        
        AudioManager.Instance.SfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void OnButtonHover(MouseEnterEvent evt)
    {
        EventHub.UIHover();
    }
    
    private void OnQuitButtonClick(ClickEvent evt)
    {
        EventHub.UIClick();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}   
}
