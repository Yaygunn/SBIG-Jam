using UnityEngine;

namespace Controller.Enemy.States
{
    public class LeaveGardenState : BusyState
    {
        public LeaveGardenState(EnemyController enemy) : base(enemy) { }
        
        private float _idleTriggerDistance = 1.5f;
        private Vector3 _runoffDirection = Vector3.zero;
        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        public override void Enter()
        {
            base.Enter();
            
            _enemy.CalculateClosestEntrance();
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_enemy.EntrancePoint != null)
            {
                float distanceToEntrance = Vector3.Distance(_enemy.transform.position, _enemy.EntrancePoint.position);
                
                if (distanceToEntrance < _idleTriggerDistance)
                {
                    if (_runoffDirection == Vector3.zero)
                    {
                        _runoffDirection = _enemy.transform.position + _enemy.transform.forward * 50f;
                    }
                    
                    // Just keep on "Running into the woods"
                    _enemy.GolemAnimator.SetFloat(SpeedHash, _enemy.NavMeshAgent.speed);
                    _enemy.NavMeshAgent.SetDestination(_runoffDirection);
                }
                else
                {
                    // If the enemy is not at the entrance point, it should be moving towards it
                    _enemy.GolemAnimator.SetFloat(SpeedHash, _enemy.NavMeshAgent.speed);
                    _enemy.NavMeshAgent.SetDestination(_enemy.EntrancePoint.position);
                }
            }
        }
    }
}