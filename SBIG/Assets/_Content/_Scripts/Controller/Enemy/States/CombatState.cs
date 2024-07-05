using UnityEngine;

namespace Controller.Enemy.States
{
    public class CombatState: ActiveState
    {
        private float attackCooldown;
        private float lastAttackTime;
        private static readonly int HeadButHash = Animator.StringToHash("HeadBut");

        public CombatState(EnemyController enemy) : base(enemy)
        {
            attackCooldown = enemy.EnemyConfig.attackCooldown;
        }

        public override void Enter()
        {
            base.Enter();

            lastAttackTime = -attackCooldown;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (_enemy.Target == null)
            {
                _enemy.ChangeState(_enemy.StateIdle);
                return;
            }
            
            float targetDistanceSq = (_enemy.transform.position - _enemy.Target.position).sqrMagnitude;

            if (targetDistanceSq > ( _enemy.EnemyConfig.attackRange * _enemy.EnemyConfig.attackRange + 1.5f))
            {
                _enemy.ChangeState(_enemy.StateChase);
            }
            else if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }

        }

        protected override void Attack()
        {
            // ##TODO:
            _enemy.GolemAnimator.SetTrigger(HeadButHash);
            
            // Apply damage to target
            
            // Temporary move to LeaveGardenState
            // _enemy.ChangeState(_enemy.StateLeaveGarden);
        }
    }
}