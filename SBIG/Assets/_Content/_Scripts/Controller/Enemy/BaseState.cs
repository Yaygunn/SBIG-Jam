namespace Controller.Enemy
{
    public abstract class BaseState
    {
        protected EnemyController enemy;

        public BaseState(EnemyController enemy)
        {
            this.enemy = enemy;
        }

        public abstract void Enter();
        public abstract void LogicUpdate();
        public abstract void PhysicUpdate();
        public abstract void Exit();
    }
}