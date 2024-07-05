using Enums.Bullet;
using UnityEngine;

namespace Components.BulletHit
{
    public class BulletHit : MonoBehaviour
    {
        public EBulletType BulletType;
        private void OnCollisionEnter(Collision other)
        {
            if (BulletType == EBulletType.Water)
            {
                if (other.gameObject.TryGetComponent(out IWaterHit waterHit))
                {
                    waterHit.OnWaterHit();
                }

                return;
            }

            if (BulletType == EBulletType.Basketball)
            {
                if (other.gameObject.TryGetComponent(out IBasketBallHit basketBallHit))
                {
                    basketBallHit.OnBasketBallHit();
                }
                
                return;
            }

            if (BulletType == EBulletType.Slap)
            {
                if (other.gameObject.TryGetComponent(out ISlapHit slapHit))
                {
                    slapHit.OnSlapHit();
                }   
                
                return;
            }

            if (BulletType == EBulletType.Golem)
            {
                if (other.gameObject.TryGetComponent(out IGolemHit golemHit))
                {
                    golemHit.OnGolemHit();
                }
                
                return;
            }
        }
    }   
}
