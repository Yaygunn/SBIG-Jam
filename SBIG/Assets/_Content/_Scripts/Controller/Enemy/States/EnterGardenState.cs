using Managers.Global;
using UnityEngine;

namespace Controller.Enemy.States
{
    public class EnterGardenState : BusyState
    {
        public EnterGardenState(EnemyController enemy) : base(enemy) { }

        private float _idleTriggerDistance = 1f;

        public override void Enter()
        {
            base.Enter();
            
            CalculateClosestEntrance();
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
                
                _enemy.NavMeshAgent.SetDestination(_enemy.EntrancePoint.position);
            }
        }

        private void CalculateClosestEntrance()
        {
            float closestDistance = float.MaxValue;
            int closestIndex = 0;
            
            for (var i = 0; i < GlobalObject.Entrances.Count; i++)
            {
                float distanceToEntrance = Vector3.Distance(_enemy.transform.position, GlobalObject.Entrances[i].position);

                if (distanceToEntrance < closestDistance)
                {
                    closestDistance = distanceToEntrance;
                    closestIndex = i;
                }
            }
            
            _enemy.EntrancePoint = GlobalObject.Entrances[closestIndex];
        }
    }
}