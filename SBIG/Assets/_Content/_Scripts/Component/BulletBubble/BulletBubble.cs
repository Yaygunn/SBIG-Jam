using System.Collections;
using Components.BulletMove;
using UnityEngine;

namespace Components.Bubble
{
    public class BulletBubble : MonoBehaviour, IBulletMove
    {
        [SerializeField] private ParticleSystem _bubbleParticles;
        
        public void Initialize()
        {
            _bubbleParticles.Play();
            
            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(15);
            
            Destroy(gameObject);
        }
    }   
}
