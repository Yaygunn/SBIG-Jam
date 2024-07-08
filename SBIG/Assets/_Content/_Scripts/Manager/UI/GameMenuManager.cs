using System;
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
        public RectTransform YouDiedMenu;
        public RectTransform PrologueMenu;
        public RectTransform PrologueContinueText;
        
        public Button RestartButton;
        public Button QuitButton;
        public Button BackButton;
        public Button SettingsButton;
        
        public Button DiedRestartButton;
        public Button DiedQuitButton;
        
        public Slider VolumeSlider;
        public Slider NarratorSlider;
        public Slider SFXSlider;
        public Slider MouseSensitivtySlider;

        private bool _settingsOpen;
        private bool _pauseOpen;
        private bool _diedOpen;
        
        private void Start()
        {
            Time.timeScale = 1;
            
            RegisterButtonListeners();
        }

        private void Update()
        {
            if (_diedOpen)
                return;
            
            // Yaygun plz don't hate me for this :D
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_settingsOpen && !_pauseOpen)
                {
                    Time.timeScale = 0;
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
                    Time.timeScale = 1;
                    InputHandler.Instance.EnableGameplayMod();
                    HidePauseMenu();
                }
            }
        }

        private void OnDisable()
        {
            EventHub.Ev_PlayerDied -= ShowDiedMenu;
            EventHub.Ev_ShowPrologueText -= ShowPrologueText;
            EventHub.Ev_ClosePrologue -= ClosePrologue;
        }

        private void ShowDiedMenu()
        {
            InputHandler.Instance.EnableUIMod();
            _diedOpen = true;
            YouDiedMenu.gameObject.SetActive(true);
        }

        private void RegisterButtonListeners()
        {
            RestartButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameLevel");
            });
            
            QuitButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainMenu");
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
            
            EventHub.Ev_PlayerDied += ShowDiedMenu;
            EventHub.Ev_ShowPrologueText += ShowPrologueText;
            EventHub.Ev_ClosePrologue += ClosePrologue;
            
            DiedRestartButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
            
            DiedQuitButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainMenu");
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

        private void ShowPrologueText()
        {
            PrologueContinueText.gameObject.SetActive(true);
        }

        private void ClosePrologue()
        {
            PrologueContinueText.gameObject.SetActive(false);
            PrologueMenu.gameObject.SetActive(false);
        }
    } 
}
