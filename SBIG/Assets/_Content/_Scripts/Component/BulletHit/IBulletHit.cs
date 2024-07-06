using UnityEngine;

namespace Components.BulletHit
{
    public interface IWaterHit
    {
        public void OnWaterHit(int damageAmount);
    }
    public interface IBasketBallHit
    {
        public void OnBasketBallHit(int damageAmount, Vector3 direction);
    }
    public interface ISlapHit
    {
        public void OnSlapHit();
    }
    public interface IGolemHit
    {
        public void OnGolemHit(int damageAmount, Vector3 direction, Vector3 hitPoint);
    }

    public interface IRockHit
    {
        public void OnRockHit(int damageAmount);
    }
    
}