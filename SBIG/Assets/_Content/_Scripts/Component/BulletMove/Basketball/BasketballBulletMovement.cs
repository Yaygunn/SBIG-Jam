using System;
using System.Collections;
using System.Collections.Generic;
using Managers.Global;
using UnityEngine;

namespace Components.BulletMove.Basketball
{
    public class BasketballBulletMovement : MonoBehaviour , IBulletMove
    {
        [SerializeField] private float Speed = 15f;
        [SerializeField] private int maxBounces = 3;
        [SerializeField] private float destroyAfter = 15f;

        private Rigidbody _rb;
        private int _bounceCount = 0;
        public void Initialize()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.velocity = transform.forward * Speed;
            
            StartCoroutine(DestroyTimer());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_bounceCount >= maxBounces)
            {
                Destroy(gameObject);
                return;
            }

            _bounceCount++;
            
            Vector3 directionToPlayer = (GlobalObject.Player.transform.position - transform.position).normalized;

            // Adjust the speed if it drops below the initial speed
            if (_rb.velocity.magnitude < Speed)
            {
                _rb.velocity = directionToPlayer * Speed;
            }
            else
            {
                _rb.velocity = directionToPlayer * _rb.velocity.magnitude;
            }

        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(destroyAfter);
            
            Destroy(gameObject);
        }
    }
}