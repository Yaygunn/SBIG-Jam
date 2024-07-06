using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Singleton;
using YInput;

namespace Managers.LevelStart
{
    public enum EStartInput { gameplay, UI}
    public class LevelStartManager : Singleton<LevelStartManager>
    {
        [SerializeField] private EStartInput _startInput;

        private void Start()
        {
            InputOperation();
            UpdateMouseSensitivity();
        }

        private void InputOperation()
        {
                if(_startInput == EStartInput.gameplay)
                {
                    InputHandler.Instance.EnableGameplayMod();
                }
                else
                {
                    InputHandler.Instance.EnableUIMod();
                }
        }

        private void UpdateMouseSensitivity()
        {
            float sensitivity = GetMouseSensitivity();
            
            SetMouseSensitivity(sensitivity);
        }

        public void SetMouseSensitivity(float sensitivity)
        {
            InputHandler.Instance.SetMouseSensitivity(sensitivity);
            PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        }

        public float GetMouseSensitivity()
        {
            return PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        }
    }
}