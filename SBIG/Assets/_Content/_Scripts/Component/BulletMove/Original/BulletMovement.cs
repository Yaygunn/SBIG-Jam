using System.Collections;
using UnityEngine;

namespace Components.BulletMove.Original
{
    public class BulletMovement : MonoBehaviour , IBulletMove
    {
        [SerializeField] private float Speed = 15;
        public void Initialize()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * Speed;
            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(15);
            
            Destroy(gameObject);
        }
    }
}