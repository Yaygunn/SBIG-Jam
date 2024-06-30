using UnityEngine;
using YInput;

namespace Controller.Player
{
    public class ActiveState : BaseState
    {
        public ActiveState(PlayerController player) : base(player) { }

        protected virtual void Move()
        {
            _player.CompMove.Move(InputHandler.Instance.MoveInput);
        }
        protected virtual void Rotate()
        {
            _player.CompRotate.Rotate(InputHandler.Instance.MousePositionChange);
        }
        protected virtual void GunUsage()
        {
            FireOp();
        }
        protected virtual void FireOp()
        {
            if (InputHandler.Instance.LeftClick.IsPressed)
                _player.CompWeaponHandle.Fire();
        }
        protected virtual void Interaction()
        {
            _player.CompCarry.LogicUpdate();

            if (InputHandler.Instance.Interact.IsPressed)
                _player.CompCarry.OnInteractButton();
            if(InputHandler.Instance.ActivateCauldron.IsPressed)
                _player.CompCarry.OnActivateCauldronButton();
        }
    }
}
