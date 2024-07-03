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
            _player.CompRotate.Rotate();
        }

        protected virtual void GunUsage()
        {
            FireOp();
        }

        protected virtual void FireOp()
        {
            _player.CompWeaponHandle.SendFireInput(InputHandler.Instance.LeftClick);
            if (InputHandler.Instance.ActivateCauldron.IsPressed)
                _player.CompWeaponHandle.Reload();
        }

        protected virtual void Interaction()
        {
            _player.CompCarry.LogicUpdate();

            if (InputHandler.Instance.Interact.IsPressed)
                _player.CompCarry.OnInteractButton();
            if (InputHandler.Instance.ActivateCauldron.IsPressed)
                _player.CompCarry.OnActivateCauldronButton();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Rotate();
            Move();
        }
    }
}