namespace Controller.Enemy
{
    public class BaseState
    {
        protected EnemyController _enemy;
        public BaseState(EnemyController enemy)
        {
            _enemy = enemy;
        }

        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicUpdate() { }
        public virtual void Exit() { }
    }
}