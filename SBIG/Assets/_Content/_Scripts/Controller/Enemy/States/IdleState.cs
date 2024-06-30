using UnityEngine;

namespace Controller.Enemy.States
{
    public class IdleState: BaseState
    {
        public IdleState(EnemyController enemy) : base(enemy) {}
        public override void Enter()
        {
            // Enter Idle State
            Debug.Log("Enter Idle State");
            
            enemy.Target = null;
        }

        public override void LogicUpdate()
        {
            // Enter Logic Update
            if (enemy.EnemyConfig.targetPlayer || enemy.EnemyConfig.targetCrops)
            {
                enemy.ChangeState(enemy.StateChase);
            }
        }

        public override void PhysicUpdate()
        {
            // Enter Physic Update
        }

        public override void Exit()
        {
            // Exit Idle State
        }
    }
}