using UnityEngine;
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

    }
}