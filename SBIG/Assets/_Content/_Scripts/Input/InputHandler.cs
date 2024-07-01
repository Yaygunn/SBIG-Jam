using UnityEngine;
using Utilities.Singleton;

namespace YInput
{
    public class InputHandler : Singleton<InputHandler>
    {
        #region Singleton
        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                Initialize();
            }
        }
        #endregion

        #region inputs

        public Vector2 MoveInput { get; private set; }

        public Vector2 MousePositionChange { get; private set; }

        public InputState LeftClick { get; private set; }

        public InputState Interact { get; private set; }

        public InputState ActivateCauldron { get; private set; }

        #endregion

        private Keys _keys;
        private void Initialize()
        {
            _keys = new Keys();

            LeftClick = new InputState(_keys.gameplay.LeftClick);
            Interact = new InputState(_keys.gameplay.Interact);
            ActivateCauldron = new InputState(_keys.gameplay.ActivateCauldron);

            EnableGameplayMod();
        }

        private void Update()
        {
            MoveInput = _keys.gameplay.move.ReadValue<Vector2>();
            MousePositionChange = _keys.gameplay.MouseMove.ReadValue<Vector2>();
        }
        private void LateUpdate()
        {
            LeftClick.ResetInputInfo();
            Interact.ResetInputInfo();
            ActivateCauldron.ResetInputInfo();
        }

        private void EnableGameplayMod()
        {
            _keys.gameplay.Enable();
        }

        private void OnDisable()
        {
            if (Instance != this)
                return;

            _keys.Disable();
        }
    }
}
