using UnityEngine;

namespace Controller.Enemy.States
{
    public class CombatState: BaseState
    {
        public CombatState(EnemyController enemy) : base(enemy) { }

        public override void Enter()
        {
            // Enter Combat State
            Debug.Log("Enter Combat State");
        }

        public override void LogicUpdate()
        {
            // Enter Logic Update
        }

        public override void PhysicUpdate()
        {
            // Enter Physic Update
        }

        public override void Exit()
        {
            // Exit Combat State
        }
    }
}