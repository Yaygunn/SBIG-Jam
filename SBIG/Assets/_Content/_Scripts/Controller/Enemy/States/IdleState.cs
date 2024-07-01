using UnityEngine;

namespace Controller.Enemy.States
{
    public class IdleState: BaseState
    {
        public IdleState(EnemyController enemy) : base(enemy) {}
        public override void Enter()
        {
            base.Enter();
            
            _enemy.Target = null;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (_enemy.EnemyConfig.targetPlayer || _enemy.EnemyConfig.targetCrops)
            {
                _enemy.ChangeState(_enemy.StateChase);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}