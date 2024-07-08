using Managers.Global;
using UnityEngine;

namespace Controller.Enemy.States
{
    public class EnterGardenState : BusyState
    {
        public EnterGardenState(EnemyController enemy) : base(enemy) { }

        private float _idleTriggerDistance = 1f;
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
                float distanceToEntranceSqr = (_enemy.transform.position - _enemy.EntrancePoint.position).sqrMagnitude;
                
                if (distanceToEntranceSqr < (_idleTriggerDistance * _idleTriggerDistance))
                {
                    _enemy.ChangeState(_enemy.StateIdle);
                    return;
                }
                
                _enemy.GolemAnimator.SetFloat(SpeedHash, _enemy.NavMeshAgent.speed);
                _enemy.NavMeshAgent.SetDestination(_enemy.EntrancePoint.position);
            }
        }
    }
}