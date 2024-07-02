namespace Controller.Enemy
{
    public class BusyState : BaseState
    {
        public BusyState(EnemyController enemy) : base(enemy) { }

        protected virtual void Move() { }

        protected virtual void Rotate() { }

        protected virtual void Attack() { }
    }
}