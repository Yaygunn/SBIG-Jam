namespace Controller.Enemy
{
    public class ActiveState : BaseState
    {
        public ActiveState(EnemyController enemy) : base(enemy) { }

        protected virtual void Move() { }

        protected virtual void Rotate() { }

        protected virtual void Attack() { }
    }
}