using Components.Crops;
using Controller.Player;
using Managers.Global;
using UnityEngine;

namespace Controller.Enemy.States
{
    public class EatCropState: ActiveState
    {
        private float attackCooldown;
        private float lastAttackTime;
        private static readonly int HeadButHash = Animator.StringToHash("HeadBut");

        public EatCropState(EnemyController enemy) : base(enemy)
        {
            attackCooldown = enemy.EnemyConfig.attackCooldown;
        }

        public override void Enter()
        {
            base.Enter();

            lastAttackTime = -attackCooldown;
            _enemy.NavMeshAgent.velocity = Vector3.zero;
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

        public override void Exit()
        {
            base.Exit();
            
            _enemy.GolemAnimator.ResetTrigger(HeadButHash);
        }

        protected override void Attack()
        {
            _enemy.GolemAnimator.SetTrigger(HeadButHash);
            _enemy.SetFaceState(Enums.Golem.EGolemState.HUNGRY);

            // ##TODO
            // We need to have a TakeDamage method in both Player and the Crop
            // But for now lets assume its only enemy

            if (_enemy.Target.GetComponent<Crop>().GiveDamage())
            {
                _enemy.ChangeState(_enemy.StateIdle);
            }
        }
    }
}