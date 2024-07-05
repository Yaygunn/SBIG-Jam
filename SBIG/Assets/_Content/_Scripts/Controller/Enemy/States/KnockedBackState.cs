using Enums.Golem;
using UnityEngine;

namespace Controller.Enemy.States
{ 
    public class KnockedBackState: ActiveState
    {
        public float KnockbackDuration = 5f;
        
        private float _force = 3f;
        private float _knockbackTime;
        private Quaternion _initialRotation;
            
        public KnockedBackState(EnemyController enemy) : base(enemy) {}
        public override void Enter()
        {
            base.Enter();
        
            _initialRotation = _enemy.transform.rotation;
            _enemy.Target = null;
            _enemy.NavMeshAgent.isStopped = true;
            _enemy.NavMeshAgent.enabled = false;
            _enemy.RB.isKinematic = false;
            _knockbackTime = Time.time + KnockbackDuration;
            _enemy.RB.AddForce(_enemy.hitDirection * _force, ForceMode.Impulse);
            _enemy.SetFaceState(EGolemState.DIZZY);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (Time.time >= _knockbackTime)
            {
                _enemy.ChangeState(_enemy.StateIdle);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            
            _enemy.transform.rotation = _initialRotation;
            _enemy.NavMeshAgent.enabled = true;
            _enemy.RB.isKinematic = true;
            _enemy.SetFaceState(EGolemState.ANGRY);
        }
    }
}