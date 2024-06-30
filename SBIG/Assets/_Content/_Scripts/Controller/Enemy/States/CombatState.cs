using UnityEngine;

namespace Controller.Enemy.States
{
    public class CombatState: BaseState
    {
        public CombatState(EnemyController enemy) : base(enemy)
        {
            attackCooldown = enemy.EnemyConfig.attackCooldown;
        }

        private float attackCooldown;
        private float lastAttackTime;

        public override void Enter()
        {
            // Enter Combat State
            Debug.Log("Enter Combat State");

            lastAttackTime = -attackCooldown;
        }

        public override void LogicUpdate()
        {
            // Enter Logic Update
            if (enemy.Target == null)
            {
                enemy.ChangeState(enemy.StateIdle);
                return;
            }
            
            float targetDistanceSq = (enemy.transform.position - enemy.Target.position).sqrMagnitude;

            if (targetDistanceSq > ( enemy.EnemyConfig.attackRange * enemy.EnemyConfig.attackRange + 1.5f))
            {
                enemy.ChangeState(enemy.StateChase);
            }
            else if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }

        }

        public override void PhysicUpdate()
        {
            // Enter Physic Update
        }

        public override void Exit()
        {
            // Exit Combat State
        }
        
        private void Attack()
        {
            Debug.Log("Attacking target: " + enemy.Target.name);
            
            // ##TODO:
            // Play attack animation
            // Apply damage to target
        }
    }
}