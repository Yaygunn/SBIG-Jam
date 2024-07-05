using Enums;
using UnityEngine;

namespace Controller.Enemy.States
{
    public class ChaseState : ActiveState
    {
        public ChaseState(EnemyController enemy) : base(enemy) { }
        
        private static readonly int Charge = Animator.StringToHash("Charge");

        public override void Enter()
        {
            base.Enter();
            
            _enemy.GolemAnimator.SetBool(Charge, _enemy.TargetType == ETargetType.PLAYER);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (_enemy.Target == null)
            {
                _enemy.ChangeState(_enemy.StateIdle);
                return;
            }
            
            _enemy.NavMeshAgent.SetDestination(_enemy.Target.position);
            
             /*CheckForPlayer();*/
            
             float targetDistanceSq = (_enemy.transform.position - _enemy.Target.position).sqrMagnitude;
             float detectionRangeSq = _enemy.EnemyConfig.detectionRange * _enemy.EnemyConfig.detectionRange;
             float combatRangeSq = _enemy.EnemyConfig.attackRange * _enemy.EnemyConfig.attackRange;

             if (targetDistanceSq <= combatRangeSq)
             {
                 _enemy.ChangeState(_enemy.StateCombat);
             }
             else if (targetDistanceSq <= detectionRangeSq)
             {
                 _enemy.GolemAnimator.SetBool(Charge, true);
                 _enemy.NavMeshAgent.SetDestination(_enemy.Target.position);
             }
             else
             {
                 _enemy.ChangeState(_enemy.StateIdle);
             }
        }

        public override void Exit()
        {
            base.Exit();
            
            _enemy.GolemAnimator.SetBool(Charge, false);
        }
    }
}
