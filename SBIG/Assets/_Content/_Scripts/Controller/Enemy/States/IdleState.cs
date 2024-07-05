using UnityEngine;

namespace Controller.Enemy.States
{
    public class IdleState: ActiveState
    {
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        public IdleState(EnemyController enemy) : base(enemy) {}
        public override void Enter()
        {
            base.Enter();
            
            _enemy.Target = null;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _enemy.GolemAnimator.SetFloat(SpeedHash, _enemy.NavMeshAgent.speed);
            
            if (_enemy.EnemyConfig.targetPlayer || _enemy.EnemyConfig.targetCrops)
            {
                _enemy.ChangeState(_enemy.StateChase);
            }
        }
    }
}