using Enums.Golem;
using UnityEngine;

namespace Controller.Enemy.States
{ 
    public class KnockedBackState: ActiveState
    {
        private float _force = 10f;
        private float _knockbackTime;
        private float _knockbackDuration = 5f;
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
            _knockbackTime = Time.time + _knockbackDuration;
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