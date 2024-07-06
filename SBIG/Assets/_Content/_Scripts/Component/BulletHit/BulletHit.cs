using System.Collections.Generic;
using Enums.Bullet;
using Scriptables.Enemy;
using UnityEngine;

namespace Components.BulletHit
{
    public class BulletHit : MonoBehaviour
    {
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
            
            if (BulletType == EBulletType.Water)
            {
                if (other.gameObject.TryGetComponent(out IWaterHit waterHit))
                {
                    waterHit.OnWaterHit(DamageAmount);
                }

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
                    Destroy(gameObject);
                }   
                
                return;
            }

            if (BulletType == EBulletType.Golem)
            {
                if (other.gameObject.TryGetComponent(out IGolemHit golemHit))
                {
                    _collider.enabled = false; // Safety measure
                    Vector3 direction = other.contacts[0].point - transform.position;
                    golemHit.OnGolemHit(DamageAmount, direction, other.contacts[0].point);
                }
                
                Destroy(gameObject);
            }
        }
    }   
}
