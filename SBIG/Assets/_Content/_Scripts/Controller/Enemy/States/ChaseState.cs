using System.Collections;
using System.Collections.Generic;
using Managers.Global;
using UnityEngine;

namespace Controller.Enemy.States
{
    public class ChaseState : BaseState
    {
        public ChaseState(EnemyController enemy) : base(enemy) { }
        
        private float _checkInterval = 0.5f;
        private float _nextCheckTime = 0f;
        
        public override void Enter()
        {
            base.Enter();
            
            GameObject playerObject = null;
            GameObject[] cropObjects = null;
            
            if (_enemy.EnemyConfig.targetPlayer)
            {
                playerObject = GlobalObject.Player;
            }

            if (_enemy.EnemyConfig.targetCrops)
            {
                // This needs to be refactored, ideally if we store a reference to all crops in the scene
                // in the GlobalObject or a Singleton for the CropManager?
                cropObjects = GameObject.FindGameObjectsWithTag("Crop");
            }
            
            if (FindClosestTarget(playerObject, cropObjects, out Transform closestTarget))
            {
                _enemy.Target = closestTarget;
            }
            
            if (_enemy.Target == null)
            {
                _enemy.ChangeState(_enemy.StateIdle);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (Time.time >= _nextCheckTime)
            {
                _nextCheckTime = Time.time + _checkInterval;

                CheckForPlayer();
                
                if (_enemy.Target != null)
                {
                    float targetDistanceSq = (_enemy.transform.position - _enemy.Target.position).sqrMagnitude;
                    float detectionRangeSq = _enemy.EnemyConfig.detectionRange * _enemy.EnemyConfig.detectionRange;
                    float combatRangeSq = _enemy.EnemyConfig.attackRange * _enemy.EnemyConfig.attackRange;

                    if (targetDistanceSq <= combatRangeSq)
                    {
                        _enemy.ChangeState(_enemy.StateCombat);
                    }
                    else if (targetDistanceSq <= detectionRangeSq)
                    {
                        _enemy.NavMeshAgent.SetDestination(_enemy.Target.position);
                    }
                    else
                    {
                        _enemy.ChangeState(_enemy.StateIdle);
                    }
                }
                else
                {
                    _enemy.ChangeState(_enemy.StateIdle);
                }
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

        private void CheckForPlayer()
        {
            if (_enemy.EnemyConfig.prioritizePlayer)
            {
                GameObject playerObject = GlobalObject.Player;
                if (playerObject != null)
                {
                    float playerDistance = Vector3.Distance(_enemy.transform.position, playerObject.transform.position);
                    if (playerDistance < Vector3.Distance(_enemy.transform.position, _enemy.Target.position))
                    {
                        _enemy.Target = playerObject.transform;
                    }
                }
            }
        }
        
        private bool FindClosestTarget(GameObject player, GameObject[] crops, out Transform target)
        {
            target = null;
            float closestDistance = _enemy.EnemyConfig.detectionRange;
            bool foundTarget = false;

            if (player != null)
            {
                float playerDistance = Vector3.Distance(_enemy.transform.position, player.transform.position);
                if (playerDistance < closestDistance)
                {
                    target = player.transform;
                    closestDistance = playerDistance;
                    foundTarget = true;

                }
            }

            // If crops are not null and not empty
            if (crops != null && crops.Length > 0)
            {
                foreach (GameObject crop in crops)
                {
                    float cropDistance = Vector3.Distance(_enemy.transform.position, crop.transform.position);
                    if (cropDistance < closestDistance)
                    {
                        target = crop.transform;
                        closestDistance = cropDistance;
                        foundTarget = true;
                    }
                }   
            }

            return foundTarget;
        }
    }
}
