namespace Controller.Player
{
    public class BaseState
    {
        protected PlayerController _player;
        public BaseState(PlayerController player)
        {
            _player = player;
        }

        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicUpdate() { }
        public virtual void Exit() { }
    }
}
