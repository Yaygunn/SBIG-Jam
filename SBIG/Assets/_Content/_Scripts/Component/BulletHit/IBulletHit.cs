using UnityEngine;

namespace Components.BulletHit
{
    public interface IWaterHit
    {
        public void OnWaterHit();
    }
    public interface IBasketBallHit
    {
        public void OnBasketBallHit(Vector3 direction);
    }
    public interface ISlapHit
    {
        public void OnSlapHit();
    }
    public interface IGolemHit
    {
        public void OnGolemHit();
    }
}