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
            // Enter Chase State
            Debug.Log("Enter Chase State");
            
            GameObject playerObject = null;
            GameObject[] cropObjects = null;
            
            if (enemy.EnemyConfig.targetPlayer)
            {
                playerObject = GlobalObject.Player;
            }

            if (enemy.EnemyConfig.targetCrops)
            {
                // This needs to be refactored, ideally if we store a reference to all crops in the scene
                // in the GlobalObject or a Singleton for the CropManager?
                cropObjects = GameObject.FindGameObjectsWithTag("Crop");
            }
            
            if (FindClosestTarget(playerObject, cropObjects, out Transform closestTarget))
            {
                enemy.Target = closestTarget;
            }
            
            if (enemy.Target == null)
            {
                enemy.ChangeState(enemy.StateIdle);
            }
        }

        public override void LogicUpdate()
        {
            // Enter Logic Update
            if (Time.time >= _nextCheckTime)
            {
                _nextCheckTime = Time.time + _checkInterval;

                CheckForPlayer();
                
                if (enemy.Target != null)
                {
                    float targetDistanceSq = (enemy.transform.position - enemy.Target.position).sqrMagnitude;
                    float detectionRangeSq = enemy.EnemyConfig.detectionRange * enemy.EnemyConfig.detectionRange;
                    float combatRangeSq = enemy.EnemyConfig.attackRange * enemy.EnemyConfig.attackRange;

                    if (targetDistanceSq <= combatRangeSq)
                    {
                        enemy.ChangeState(enemy.StateCombat);
                    }
                    else if (targetDistanceSq <= detectionRangeSq)
                    {
                        enemy.NavMeshAgent.SetDestination(enemy.Target.position);
                    }
                    else
                    {
                        enemy.ChangeState(enemy.StateIdle);
                    }
                }
                else
                {
                    enemy.ChangeState(enemy.StateIdle);
                }
            }
        }

        public override void PhysicUpdate()
        {
            // Enter Physic Update
        }

        public override void Exit()
        {
            // Exit Chase State
        }

        private void CheckForPlayer()
        {
            if (enemy.EnemyConfig.prioritizePlayer)
            {
                GameObject playerObject = GlobalObject.Player;
                if (playerObject != null)
                {
                    float playerDistance = Vector3.Distance(enemy.transform.position, playerObject.transform.position);
                    if (playerDistance < Vector3.Distance(enemy.transform.position, enemy.Target.position))
                    {
                        enemy.Target = playerObject.transform;
                    }
                }
            }
        }
        
        private bool FindClosestTarget(GameObject player, GameObject[] crops, out Transform target)
        {
            target = null;
            float closestDistance = enemy.EnemyConfig.detectionRange;
            bool foundTarget = false;

            if (player != null)
            {
                float playerDistance = Vector3.Distance(enemy.transform.position, player.transform.position);
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
                    float cropDistance = Vector3.Distance(enemy.transform.position, crop.transform.position);
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
