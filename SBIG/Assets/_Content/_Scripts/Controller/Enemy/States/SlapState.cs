using System.Collections;
using System.Collections.Generic;
using Enums.Golem;
using Managers.Global;
using UnityEngine;

namespace Controller.Enemy.States
{
    public class SlapState : BusyState
    {
        public SlapState(EnemyController enemy) : base(enemy) {}
        
        private float _slapWaitTimer = 5f;
        private float _slapWaitTime;
        private float _rotateSpeed = 2f;
        private Vector3 _lookDirection;
        private Quaternion _lookRotation;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public override void Enter()
        {
            base.Enter();
            
            _enemy.Target = GlobalObject.Player.transform;
            _enemy.NavMeshAgent.isStopped = true;
            _enemy.NavMeshAgent.velocity = Vector3.zero;
            _enemy.NavMeshAgent.ResetPath();
            _enemy.SetFaceState(EGolemState.ANGRY);
            _enemy.NavMeshAgent.velocity = Vector3.zero;
            _enemy.GolemAnimator.SetFloat(Speed, 0);
            
            _slapWaitTime = Time.time + _slapWaitTimer;
            
            _lookDirection = (_enemy.Target.position - _enemy.transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(new Vector3(_lookDirection.x, 0, _lookDirection.z));
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, _lookRotation, Time.deltaTime * _rotateSpeed);
            
            if (Time.time >= _slapWaitTime)
            {
                _enemy.ChangeState(_enemy.StateLeaveGarden);
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }   
}
