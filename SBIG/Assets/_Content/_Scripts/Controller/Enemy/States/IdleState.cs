using Enums;
using Managers.Global;
using UnityEngine;

namespace Controller.Enemy.States
{
    public class IdleState: ActiveState
    {
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        public IdleState(EnemyController enemy) : base(enemy) {}
        
        private float _seekTargetInterval = 2f;
        private float _nextSeekTime = 0f;
        private bool _targetFound;
        
        public override void Enter()
        {
            base.Enter();
            
            _enemy.Target = null;
            _enemy.GolemAnimator.SetFloat(SpeedHash, 0f);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (_enemy.EnemyConfig.targetPlayer || _enemy.EnemyConfig.targetCrops)
            {
                if (Time.time >= _nextSeekTime)
                {
                    _nextSeekTime = Time.time + _seekTargetInterval;

                    SeekTarget();

                    if (_targetFound)
                    {
                        _enemy.ChangeState(_enemy.StateChase);
                    }
                }
            }
        }

        private void SeekTarget()
        {
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
            
            _targetFound = _enemy.Target != null;
        }
        
        /*private void CheckForPlayer()
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
        }*/
        
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
                    _enemy.TargetType = ETargetType.PLAYER;
                }
            }
            
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
                        _enemy.TargetType = ETargetType.CROP;
                    }
                }   
            }

            return foundTarget;
        }
    }
}