using Manager.Audio;
using Managers.LevelStart;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utilities.Singleton;
using YInput;

namespace Manager.UI
{
    public class GameMenuManager : Singleton<GameMenuManager>
    {
        public RectTransform PauseMenu;
        public RectTransform SettingsMenu;
        
        public Button RestartButton;
        public Button QuitButton;
        public Button BackButton;
        public Button SettingsButton;
        
        public Slider VolumeSlider;
        public Slider NarratorSlider;
        public Slider SFXSlider;
        public Slider MouseSensitivtySlider;

        private bool _settingsOpen;
        private bool _pauseOpen;
        
        private void Start()
        {
            RegisterButtonListeners();
        }

        private void Update()
        {
            // Yaygun plz don't hate me for this :D
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_settingsOpen && !_pauseOpen)
                {
                    InputHandler.Instance.EnableUIMod();
                    ShowPauseMenu();
                }
                else if (_settingsOpen)
                {
                    HideSettingsMenu();
                    ShowPauseMenu();
                }
                else if (_pauseOpen)
                {
                    InputHandler.Instance.EnableGameplayMod();
                    HidePauseMenu();
                }
            }
        }

        private void RegisterButtonListeners()
        {
            RestartButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
            
            QuitButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
            
            BackButton.onClick.AddListener(() =>
            {
                HideSettingsMenu();
                ShowPauseMenu();
            });
            
            SettingsButton.onClick.AddListener(() =>
            {
                HidePauseMenu();
                ShowSettingsMenu();
            });
            
            VolumeSlider.onValueChanged.AddListener((value) =>
            {
                AudioManager.Instance.MusicVolume = value;
                PlayerPrefs.SetFloat("MusicVolume", value);
            });
            
            NarratorSlider.onValueChanged.AddListener((value) =>
            {
                AudioManager.Instance.NarratorVolume = value; 
                PlayerPrefs.SetFloat("NarrationVolume", value);
            });
            
            SFXSlider.onValueChanged.AddListener((value) =>
            {
                AudioManager.Instance.SfxVolume = value;
                PlayerPrefs.SetFloat("SFXVolume", value);
            });
            
            MouseSensitivtySlider.onValueChanged.AddListener((value) =>
            {
                LevelStartManager.Instance.SetMouseSensitivity(value);
            });
        }

        public void ShowPauseMenu()
        {
            _pauseOpen = true;
            PauseMenu.gameObject.SetActive(true);
        }
        
        public void HidePauseMenu()
        {
            _pauseOpen = false;
            PauseMenu.gameObject.SetActive(false);
        }
        
        public void ShowSettingsMenu()
        {
            _settingsOpen = true;
            SettingsMenu.gameObject.SetActive(true);
            LoadSettingsValues();
        }
        
        public void HideSettingsMenu()
        {
            _settingsOpen = false;
            SettingsMenu.gameObject.SetActive(false);
        }
        
        private void LoadSettingsValues()
        {
            MouseSensitivtySlider.value = LevelStartManager.Instance.GetMouseSensitivity();
            VolumeSlider.value = AudioManager.Instance.MusicVolume;
            NarratorSlider.value = AudioManager.Instance.NarratorVolume;
            SFXSlider.value = AudioManager.Instance.SfxVolume;
        }
    } 
}
