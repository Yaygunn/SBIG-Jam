using System.Collections.Generic;
using Controller.Enemy;
using Enums.Bullet;
using Enums.Golem;
using FMODUnity;
using Manager.Enemy;
using Scriptables.Enemy;
using UnityEngine;

namespace Components.BulletHit
{
    public class BulletHit : MonoBehaviour
    {
        [SerializeField] private EventReference HitSound;
        public EBulletType BulletType;
        public int DamageAmount = 5;
        private HashSet<GameObject> _hitTargets = new HashSet<GameObject>();
        private Collider _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            // Don't allow a bullet to do damage to the same target more than once
            // This is to prevent multiple onCollisionEnter calls
            if (!_hitTargets.Contains(other.gameObject)) {
                _hitTargets.Add(other.gameObject);
            } else {
                return;
            }

            PlayHitSound();

            if (BulletType == EBulletType.Water)
            {
                if (other.gameObject.TryGetComponent(out IWaterHit waterHit))
                {
                    waterHit.OnWaterHit(DamageAmount);
                }

                Destroy(gameObject);
            }
            
            if (BulletType == EBulletType.Rock)
            {
                if (other.gameObject.TryGetComponent(out IRockHit rockHit))
                {
                    rockHit.OnRockHit(DamageAmount);
                }
                Destroy(gameObject);

                return;
            }

            if (BulletType == EBulletType.Basketball)
            {
                if (other.gameObject.TryGetComponent(out IBasketBallHit basketBallHit))
                {
                    Vector3 direction = other.contacts[0].point - transform.position;
                    basketBallHit.OnBasketBallHit(DamageAmount, direction);
                }

                return;
            }

            if (BulletType == EBulletType.Slap)
            {
                if (other.gameObject.TryGetComponent(out ISlapHit slapHit))
                {
                    slapHit.OnSlapHit();
                    EventHub.Slapped();
                    Destroy(gameObject);
                }   
                Destroy(gameObject);

                return;
            }

            if (BulletType == EBulletType.Golem)
            {
                if (other.gameObject.TryGetComponent(out IGolemHit golemHit))
                {
                    _collider.enabled = false; // Safety measure
                    Vector3 direction = other.contacts[0].point - transform.position;
                    golemHit.OnGolemHit(DamageAmount, direction, other.contacts[0].point);
                    EnemyController theRock = EnemyManager.Instance.SpawnEnemy(EGolemType.ROCK, other.contacts[0].point);
                    theRock.ChangeState(theRock.StateIdle);
                }
                else
                {

                EnemyController theRock = EnemyManager.Instance.SpawnEnemy(EGolemType.ROCK, other.contacts[0].point);
                theRock.ChangeState(theRock.StateIdle);
                }
                Destroy(gameObject);
            }
        }
    
        private void PlayHitSound()
        {
            if( ! HitSound.IsNull)
            RuntimeManager.PlayOneShot(HitSound);
        }
    }  
    
    
}
